using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastAPI.Model
{
    public class WeatherForecast : IEntity<int>, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UtcOffset { get; set; }
        [Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Temperature { get; set; }
        [Required]
        public bool UpToDate { get; set; }
        [Required]
        public DateTime ForecastTime { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        public City City { get; set; }
    }
}
