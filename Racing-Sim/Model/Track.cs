using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name)
        {
            Name = name;
        }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);

        }

        public LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            var linkedList = new LinkedList<Section>();
   
            foreach (SectionTypes sectie in sections)
            {
             
                Section Sectie = new Section(sectie);
                linkedList.AddLast(Sectie);
            }

            return linkedList;
        }


    }
}
