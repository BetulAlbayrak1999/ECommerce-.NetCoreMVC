using System;
using DataAccessLayer.Domains.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domains
{
    public class Product : BaseEntity
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public int Stoke { get; set; }
        public float Discount_Percentage { get; set; }
    }
}
