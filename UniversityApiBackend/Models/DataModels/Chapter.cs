using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Chapter : BaseEntity
    {
        [Required]
        public string Chapters { get; set; } = string.Empty;

        //Asociamos un temario a un solo curso
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
    }
}
