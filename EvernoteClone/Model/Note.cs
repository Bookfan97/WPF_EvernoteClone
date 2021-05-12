using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.Model
{
    internal class Note
    {
        public int ID { get; set; }
        public int NotebookID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
    }
}