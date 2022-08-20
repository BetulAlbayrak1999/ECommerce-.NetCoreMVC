using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.ProductDtos
{
    public class GetAllProductRequestDto
    {
        public Guid Id  { get; set; }
        public bool IsActive { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        
    }
}
