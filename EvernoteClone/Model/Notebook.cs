﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.Model
{
    internal class Notebook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}