using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductDTO
    {
       
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public float Weight { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
