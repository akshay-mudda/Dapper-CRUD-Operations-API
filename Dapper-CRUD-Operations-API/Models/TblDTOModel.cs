using System.ComponentModel.DataAnnotations;

namespace Dapper_CRUD_Operations_API.Models
{
    public class TblDTOModel
    {
        [Required , MaxLength(100)]
        public string? Column1 { get; set; }

        [Required, MaxLength(100)]
        public string? Column2 { get; set; }

        [Required, MaxLength(100)]
        public string? Column3 { get; set; }

        [Required]
        public decimal Column4 { get; set; }

        [MaxLength(100)]
        public string? Column5 { get; set; }
    }
}
