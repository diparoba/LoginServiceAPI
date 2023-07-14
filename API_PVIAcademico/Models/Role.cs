using System.ComponentModel.DataAnnotations;

namespace API_PVIAcademico.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime Add { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
