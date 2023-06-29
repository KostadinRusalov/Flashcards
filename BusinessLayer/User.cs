using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public ICollection<Folder> Folders { get; set; }

        public User()
        {
            Folders = new List<Folder>();
        }

        public User(string name, string email, string password) : this()
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
