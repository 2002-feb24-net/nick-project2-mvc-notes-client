using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotesClient.ServiceAccess.Models
{
    public class NewNote
    {
        public int AuthorId { get; set; }

        [Required]
        public string Text { get; set; }

        public List<string> Tags { get; set; }
    }
}
