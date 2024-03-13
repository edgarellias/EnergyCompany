using System.ComponentModel.DataAnnotations;

namespace EnergyCompany.WebAPI.Models
{
    public class Endpoint
    {
        
        [Key]
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public int MeterModelId { get; set; }
        [Required]
        public int MeterNumber { get; set; }
        [Required]
        public string MeterFirmwareVersion { get; set; }
        [Required]
        public int SwitchState { get; set; }


    }
}
