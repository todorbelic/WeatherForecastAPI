using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Model
{
    public class City : IEntity<int>, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; }
        [Required]
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }
        [Required]
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Latitude { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
