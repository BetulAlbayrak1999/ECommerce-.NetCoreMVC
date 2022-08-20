using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.CategoryDtos
{
    public class GetAllCategoryRequestDto
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
