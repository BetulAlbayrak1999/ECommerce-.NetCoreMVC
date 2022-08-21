using AutoMapper;
using BussinessLogicLayer.Configrations.Exceptions;
using BussinessLogicLayer.Configrations.Responses;
using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Validations.FluentValidations.ApplicationUser;
using DataAccessLayer.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Concrete
{
    public class ApplicationUserService : IApplicationUserService
    {

        private readonly IMapper _autoMapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ApplicationUserService(IMapper autoMapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _autoMapper = autoMapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Activate
        public async Task<CommandResponse> ActivateAsync(Guid Id)
        {
            try
            {
                ApplicationUser item = await _userManager.FindByIdAsync(Id.ToString());
                if (item == null)
                    return null;
                item.IsActive = true;
                var IsUpdated = await _userManager.UpdateAsync(item);
                if (IsUpdated is not null)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
            }
            catch (Exception ex)
            {
                return new CommandResponse { Status = false, Message = ex.Message };
            }
        }
        #endregion


        #region UnActivate
        public async Task<CommandResponse> UnActivateAsync(Guid Id)
        {
            try
            {
                ApplicationUser item = await _userManager.FindByIdAsync(Id.ToString());
                if (item == null)
                    return null;
                item.IsActive = false;
                var IsUpdated = await _userManager.UpdateAsync(item);
                if (IsUpdated is not null)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
            }
            catch (Exception ex)
            {
                return new CommandResponse { Status = false, Message = ex.Message };
            }
        }
        #endregion

        #region GetAll
        public async Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllAsync()
        {
            try
            {
                var users = await _userManager.Users.Select(user => new GetAllApplicationUserRequestDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email
                }).ToListAsync();

                if (users.Any())
                    return users.DefaultIfEmpty();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region GetAllActivated
        public async Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllActivatedAsync()
        {
            try
            {
                var users = await _userManager.Users.Where(x => x.IsActive == true).Select(user => new GetAllApplicationUserRequestDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    }).ToListAsync();

                if (users.Any())
                    return users.DefaultIfEmpty();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        #region GetAllUnActivated

        public async Task<IEnumerable<GetAllApplicationUserRequestDto>> GetAllUnActivatedAsync()
        {
            try
            {
                var users = await _userManager.Users.Where(x => x.IsActive == false).Select(user => new GetAllApplicationUserRequestDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                }).ToListAsync();

                if (users.Any())
                    return users.DefaultIfEmpty();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        /*#region Update

        public async Task<CommandResponse> UpdateAsync(UpdateApplicationUserRequestDto item)
        {
            try
            {
                var getItem = await _userManager.FindByIdAsync(item.Id.ToString());
                if (getItem is null)
                    return new CommandResponse { Status = false, Message = "This operation has not done successfully" };

                //validation
                var validator = new UpdateApplicationUserRequestValidator();
                validator.Validate(item).throwIfValidationException();
                    
,               var userWithSameEmail = await _userManager.FindByEmailAsync(item.Email);

                if (userWithSameEmail is not null && userWithSameEmail.Id != item.Id)
                    return new CommandResponse { Status = false, Message = "this Email is already assigned to another user" };


                var userWithSameUsername = await _userManager.FindByNameAsync(item.UserName);

                if (userWithSameUsername is not null && userWithSameUsername.Id != item.Id)
                    return new CommandResponse { Status = false, Message = "this UserName is already assigned to another user" };

                    
                //mapping
                ApplicationUser mappedItem = _autoMapper.Map<ApplicationUser>(item);
                   
                var IsUpdated = await _userManager.UpdateAsync(mappedItem);

                if (IsUpdated is not null)
                    return new CommandResponse { Status = true, Message = "This operation has not done successfully" };

                return new CommandResponse { Status = false, Message = "This operation has not done successfully" };
               
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        #endregion
        */


        #region  GetByIdAsync
        public async Task<GetApplicationUserRequestDto> GetByIdAsync(Guid Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user is null)
                    return null;


                GetApplicationUserRequestDto result = _autoMapper.Map<ApplicationUser, GetApplicationUserRequestDto>(user);
                if (result is null)
                    return null;

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
