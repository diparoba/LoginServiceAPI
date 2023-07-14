using System.ComponentModel.DataAnnotations;

namespace API_PVIAcademico.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Status { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public bool? IsProvider { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
