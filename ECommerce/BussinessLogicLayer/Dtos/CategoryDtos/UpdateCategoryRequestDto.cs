﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.CategoryDtos
{
    public class UpdateCategoryRequestDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
