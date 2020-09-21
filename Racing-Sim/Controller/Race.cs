using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random;

        private Dictionary<Section, SectionData> _positions;



        public SectionData getSectionData(Section section)
        {
            SectionData value = _positions[section];
            if(!_positions.ContainsKey(section))
            {
                
                _positions.Add(section, new SectionData());
            }
            return value;
        }
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
        }

        public void RandomizeEquipment()
        {
            foreach( IParticipant deelnemer in Participants)
            {
                deelnemer.Equipment.Quality = _random.Next(0,10);
                deelnemer.Equipment.Performance = _random.Next(0, 10);
            }
        }

    }
}
