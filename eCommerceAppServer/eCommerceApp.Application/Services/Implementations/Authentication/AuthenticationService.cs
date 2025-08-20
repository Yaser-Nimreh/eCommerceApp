using AutoMapper;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Services.Interfaces.Validation;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Repositories.Authentication;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementations.Authentication
{
    public class AuthenticationService
        (IUserManagement userManagement, IRoleManagement roleManagement, ITokenManagement tokenManagement,
        IApplicationLogger<AuthenticationService> logger, IMapper mapper,
        IValidator<CreateUser> createUserValidator, IValidator<LoginUser> loginUserValidator,
        IValidationService validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUserAsync(CreateUser user)
        {
            var validationResult = await validationService.ValidateAsync(user, createUserValidator);
            if (!validationResult.Success) return validationResult;

            var appUser = mapper.Map<ApplicationUser>(user);
            appUser.UserName = user.Email;
            appUser.PasswordHash = user.Password;

            var result = await userManagement.CreateUserAsync(appUser);
            if (!result)
                return new ServiceResponse { Message = "Email Address might be already in use or unknown error occurred" };

            var _user = await userManagement.GetUserByEmailAsync(user!.Email!);
            var users = await userManagement.GetAllUsersAsync();
            bool assignedRoleResult = await roleManagement.AddUserToRoleAsync(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedRoleResult)
            {
                // Remove user
                int removeUserResult = await userManagement.RemoveUserByEmailAsync(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    // Error occurred while rolling back changes
                    // then log the error
                    logger.LogError
                        (new Exception($"User with Email as {_user.Email} failed to be removed as a result of role assigning issue"),
                        "User could not be assigned Role");
                    return new ServiceResponse { Message = "Error occurred while creating the account" };
                }
            }

            return new ServiceResponse { Success = true, Message = "Account created!" };

            // Verify Email
        }

        public async Task<LoginResponse> LoginUserAsync(LoginUser user)
        {
            var validationResult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!validationResult.Success) return new LoginResponse(Message: validationResult.Message);

            var appUser = mapper.Map<ApplicationUser>(user);
            appUser.PasswordHash = user.Password;

            bool loginResult = await userManagement.LoginUserAsync(appUser);
            if (!loginResult)
                return new LoginResponse(Message: "Email not found or Invalid credentials");

            var _user = await userManagement.GetUserByEmailAsync(user!.Email!);
            var claims = await userManagement.GetUserClaimsAsync(_user!.Email!);

            string jwtToken = tokenManagement.GenerateToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();
            var userRefreshTokenCheck = await tokenManagement.ValidateRefreshTokenAsync(refreshToken);

            int saveRefreshTokenResult;
            if (userRefreshTokenCheck)
                saveRefreshTokenResult = await tokenManagement.UpdateRefreshTokenAsync(_user.Id, refreshToken);
            else
                saveRefreshTokenResult = await tokenManagement.AddRefreshTokenAsync(_user.Id, refreshToken);

            return saveRefreshTokenResult <= 0 ? new LoginResponse(Message: "Internal error occurred while authenticating") :
                new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);
        }

        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            bool validateRefreshTokenResult = await tokenManagement.ValidateRefreshTokenAsync(refreshToken);
            if (!validateRefreshTokenResult)
                return new LoginResponse(Message: "Invalid refresh token");

            string userId = await tokenManagement.GetUserIdByRefreshTokenAsync(refreshToken);
            var user = await userManagement.GetUserByIdAsync(userId);
            var claims = await userManagement.GetUserClaimsAsync(user!.Email!);

            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();

            await tokenManagement.UpdateRefreshTokenAsync(userId, newRefreshToken);

            return new LoginResponse(Success: true, Token: newJwtToken, RefreshToken: newRefreshToken);
        }
    }
}