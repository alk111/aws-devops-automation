using System.ComponentModel.DataAnnotations;

namespace OMartDomain.Models.User.RequestAndResponse
{
    public class UpdateUserLocationRequest
    {
        [Required]
        public string userId { get; set; }
        
        public string? permanent_address { get; set; }
        
        public string? residential_address { get; set; }
        
        public string? country { get; set; }
    }
}