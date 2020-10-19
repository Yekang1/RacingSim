using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public IParticipant.TeamColors TeamColor { get; set; }
        public int Laps { get; set; }

        public Driver (string name)
        {
            Car car = new Car();
            Name = name;
            Equipment = car;
            Laps = -1;
           
        }
        
    }
}
