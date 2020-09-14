﻿using System;
using System.Collections.Generic;
using System.Text;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, LinkedList<Section> sections)
        {
            Name = name;
            Sections = sections;
        }

        public Track(string name, SectionTypes[] sections)
        {
            
        }

        
    }
}
