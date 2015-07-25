namespace Oracle.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PRODUCTS")]
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }

        public int VendorID { get; set; }

        public string ProductName { get; set; }

        public int MeasureID { get; set; }

        public double Price { get; set; }

        public virtual MeasureDTO Measure { get; set; }

        public virtual VendorDTO Vendor { get; set; }
    }
}
