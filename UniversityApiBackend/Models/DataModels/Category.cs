using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        //Para decir que una categoria puede pertenecer a 0 o n cursos
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
