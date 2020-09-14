using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track : Section
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {

        }

    }
}
