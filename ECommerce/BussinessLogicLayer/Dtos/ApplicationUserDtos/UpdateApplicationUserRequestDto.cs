using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.ApplicationUserDtos
{
    public class UpdateApplicationUserRequestDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public bool IsActive { get; set; } = false;

        public string Email { get; set; }

        public string UserName { get; set; }
    }
}
