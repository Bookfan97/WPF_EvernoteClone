using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EvernoteClone.Model
{
    public interface HasID
    {
        public string ID { get; set; }
    }

    public class Note : HasID
    {
        public string ID { get; set; }
        public string NotebookID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
    }
}