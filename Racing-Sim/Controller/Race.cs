using Model;
using System;
using System.Collections.Generic;
using System.Timers;
using static Model.Section;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random;

        private Dictionary<Section, SectionData> _positions;

        private Timer _timer;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            StartPositieDeelnemer(track, participants);
           
            _timer = new Timer(500);
            _timer.Elapsed += onTimedEvent;
            Start();


        }



        public SectionData getSectionData(Section section)
        {

            if (!_positions.ContainsKey(section))
            {

                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }


        public void RandomizeEquipment()
        {
            foreach (IParticipant deelnemer in Participants)
            {
                deelnemer.Equipment.Quality = _random.Next(0, 10);
                deelnemer.Equipment.Performance = _random.Next(0, 10);
            }
        }

        public void StartPositieDeelnemer(Track track, List<IParticipant> participants)
        {
            foreach (Section section in track.Sections)
            {
                if (section.SectionType.Equals(SectionTypes.StartGrid))
                {
                    var sectionData = getSectionData(section);
                    foreach (IParticipant participant in participants)
                    {
                        if (sectionData.Left == null)
                        {
                            sectionData.Left = participant;
                        }
                        else if (sectionData.Right == null)
                        {
                            sectionData.Right = participant;
                        }
                        else
                        {
                            //nog doen, Meer dan twee participants
                        }
                    }

                }

            }

        }
        public void BeweegDeelnemer(Track track, List<IParticipant> participants)
        {
            foreach (Section section in track.Sections)
            {
                var sectionData = getSectionData(section);
                if (section.SectionType.Equals(SectionTypes.StartGrid))
                {
                    sectionData.Left = null;
                    sectionData.Right = null;

                }
                foreach (IParticipant participant in participants)
                {
                    if (sectionData.Left != null)
                    {
                        sectionData.Left = null;
                    }
                    else if (sectionData.Right != null)
                    {
                        sectionData.Right = null;
                    }
                    else if (sectionData.Left == null)
                    {
                        sectionData.Left = participant;
                    }
                    else if (sectionData.Left == null)
                    {
                        sectionData.Right = participant;
                    }
                    else
                    {
                        //nog doen, Meer dan twee participants
                    }
                }
            }
        }
        public void onTimedEvent(object sender, EventArgs e)
        {
            BeweegDeelnemer(Data.CurrentRace.Track, Data.CurrentRace.Participants);
        }
        public void Start()
        {
            _timer.Start();

        }


        public virtual void OnDriversChanged()
        {
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }

    }
}
