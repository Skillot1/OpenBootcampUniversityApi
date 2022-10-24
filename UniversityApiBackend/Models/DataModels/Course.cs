using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
     
        [Required]
        public Level Level { get; set; } = Level.Basic;

        //Para decir que un curso tiene 0 o N categorías
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        //Un curso puede tener 0 o n alumnos
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();

        //Un curso tiene que tener un temario
        [Required]
        public Chapter Chapter { get; set; } = new Chapter();
    }

    public enum Level { Basic, Intermediate, Advanced, Expert }

}
