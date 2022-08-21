using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Dtos.OrderDtos
{
    public class UpdateOrderRequestDto
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public bool IsActive { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
