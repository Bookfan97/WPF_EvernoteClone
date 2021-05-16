using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EvernoteClone.Model
{
    public class Notebook : HasID
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}