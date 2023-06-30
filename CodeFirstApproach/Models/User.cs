using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproach.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}
