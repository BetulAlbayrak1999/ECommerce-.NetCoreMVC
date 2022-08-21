using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.ApplicationUserDtos
{
    public class GetAllApplicationUserRequestDto
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
