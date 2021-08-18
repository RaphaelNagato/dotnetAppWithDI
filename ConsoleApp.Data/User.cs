using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Data
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
    }
}