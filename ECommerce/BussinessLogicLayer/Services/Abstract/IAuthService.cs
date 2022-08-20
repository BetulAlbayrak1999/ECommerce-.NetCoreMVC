using BussinessLogicLayer.ViewModels.AuthViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.Abstract
{
    public interface IAuthService
    {
        Task<AuthVM> RegisterAsync(RegisterVM model);

        Task<AuthVM> GetTokenAsync(TokenRequestVM model);

        Task<AuthVM> ConfirmEmailAsync(string userId, string token);


    }
}
