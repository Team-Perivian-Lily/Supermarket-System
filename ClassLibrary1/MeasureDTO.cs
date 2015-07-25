namespace Oracle.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MEASURES")]
    public class MeasureDTO
    {
        public MeasureDTO()
        {
            this.Products = new HashSet<ProductDTO>();
        }

        [Key]
        public int Id { get; set; }

        public string MeasureName { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
