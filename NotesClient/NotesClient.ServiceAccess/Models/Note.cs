using System;
using System.Collections.Generic;

namespace NotesClient.ServiceAccess.Models
{
    public class Note
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime DateModified { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
