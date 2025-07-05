using System.ComponentModel.DataAnnotations;

namespace API_Consumer.Models
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }


    public class LoginResultDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}
