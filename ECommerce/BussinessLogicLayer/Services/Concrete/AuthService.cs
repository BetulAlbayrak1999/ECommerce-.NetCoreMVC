using AutoMapper;
using BussinessLogicLayer.Configrations.Exceptions;
using BussinessLogicLayer.Helpers;
using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Validations.FluentValidations.AuthValidations;
using BussinessLogicLayer.ViewModels.AuthViewModels;
using DataAccessLayer.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public class AuthService : IAuthService
    {
        #region Ctor 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IMapper _autoMapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, IMapper autoMapper, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _autoMapper = autoMapper;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        #endregion

        #region GetTokenAsync
        public async Task<AuthVM> GetTokenAsync(TokenRequestVM model)
        {
            try
            {
                //validation
                var validator = new TokenRequestVMValidator();
                validator.Validate(model).throwIfValidationException();

                var AuthVM = new AuthVM();

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthVM.Message = "TurkishIdentity, Email or Password is incorrect";
                    return AuthVM;
                }

                var jwtSecurityToken = await CreateJwtToken(user);
                var rolesList = await _userManager.GetRolesAsync(user);

                AuthVM.IsAuthenticated = true;
                AuthVM.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                AuthVM.Email = user.Email;
                AuthVM.UserName = user.UserName;
                AuthVM.ExpiresOn = jwtSecurityToken.ValidTo;
                AuthVM.Roles = rolesList.ToList();

                return AuthVM;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        #endregion

        #region RegisterAsync
        public async Task<AuthVM> RegisterAsync(RegisterVM model)
        {
            try
            {
                //validation
                var validator = new RegisterVMValidator();
                validator.Validate(model).throwIfValidationException();

                if (await _userManager.FindByEmailAsync(model.Email) is not null)
                    return new AuthVM { Message = "Email is already registered!" };

                if (await _userManager.FindByNameAsync(model.UserName) is not null)
                    return new AuthVM { Message = "Username is already registered!" };

                //mapping between RegisterVM and applicationUser
                var user = _autoMapper.Map<ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password); //hashing for password

                if (!result.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                        errors += $"{error.Description}, ";

                    return new AuthVM { Message = errors };
                }

                //for confirming the email
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
              
                await _userManager.AddToRoleAsync(user, "Admin");

                var jwtSecurityToken = await CreateJwtToken(user);
                AuthVM AuthVM = new AuthVM //if everything is ok and IsAuthenticated is true I don't need to return messages
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "Admin" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    UserName = user.UserName
                };

                return AuthVM;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ConfirmEmailAsync
        public async Task<AuthVM> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AuthVM
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new AuthVM
                {
                    Message = "Email confirmed successfully!",
                    IsAuthenticated = true,
                };

            var errors = string.Empty;
            foreach (var error in result.Errors)
                errors += $"{error.Description}, ";
            return new AuthVM
            {
                IsAuthenticated = false,
                Message = errors
            };
        }

        #endregion


        #region CreateJwtToken
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
                rolesClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("userid", user.Id)
            }.Union(userClaims)
             .Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        #endregion

    }
}
