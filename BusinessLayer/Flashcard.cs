using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Flashcard 
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Frontside { get; set; }
        [Required]
        public string Backside { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Flashcard()
        {
            Date = DateTime.Now;
        }

        public Flashcard(string frontside, string backside) : this()
        {
            Frontside = frontside;
            Backside = backside;
        }
    }
}
