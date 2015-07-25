namespace Oracle.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VENDORS")]
    public class VendorDTO
    {
        public VendorDTO()
        {
            this.Products = new HashSet<ProductDTO>();
        }

        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
