using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Folder
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<Flashcard> Flashcards { get; set; }

        public Folder()
        {
            Flashcards = new List<Flashcard>();

        }

        public Folder(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }
    }
}
