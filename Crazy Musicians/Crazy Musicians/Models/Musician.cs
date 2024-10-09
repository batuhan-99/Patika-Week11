using System.ComponentModel.DataAnnotations;

namespace Crazy_Musicians.Models
{
    public class Musician
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Job is required")]

        public string Job {  get; set; }
        public string Skill { get; set; }

    }
}
