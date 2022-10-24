using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        //Un estudiante puede estar matriculado en 0 o varios cursos
        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
