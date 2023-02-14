using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SQL_Dependency.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
