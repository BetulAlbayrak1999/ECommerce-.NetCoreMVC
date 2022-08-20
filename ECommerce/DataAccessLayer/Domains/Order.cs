using DataAccessLayer.Domains.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domains
{
    public class Order : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
