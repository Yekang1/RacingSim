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

        private int Lengte = 100;

        private int MaxLaps = 1;


        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            StartPositieDeelnemer(track, participants);
            RandomizeEquipment();
            _timer = new Timer(200);
            _timer.Elapsed += onTimedEvent;
            Start();
        }



        public SectionData GetSectionData(Section section)
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
                deelnemer.Equipment.Quality = _random.Next(1, 10);
                deelnemer.Equipment.Performance = _random.Next(1, 10);
            }
        }

        public void StartPositieDeelnemer(Track track, List<IParticipant> participants)
        {
            foreach (IParticipant participant in participants)
            {
                bool set = false;
                foreach (Section section in track.Sections)
                {
                    if (section.SectionType.Equals(SectionTypes.StartGrid) && set == false)
                    {
                        var sectionData = GetSectionData(section);

                        if (sectionData.Left == null)
                        {
                            sectionData.Left = participant;
                            set = true;
                        }
                        else if (sectionData.Right == null)
                        {
                            sectionData.Right = participant;
                            set = true;
                        }
                    }

                }
            }

        }

        public void onTimedEvent(object sender, EventArgs e)
        {
            LinkedListNode<Section> linkedListNode = Track.Sections.Last;
            SectionData sectionNext = null;

            while (linkedListNode != null)
            {
                SectionData sectiondata = Data.CurrentRace.GetSectionData(linkedListNode.Value);

                if (sectiondata.Left != null)
                {
                    int distance = sectiondata.Left.Equipment.Speed * sectiondata.Left.Equipment.Quality;
                    sectiondata.DistanceLeft += distance;
                    if ((sectiondata.DistanceLeft) > Lengte)
                    {
                        if (linkedListNode.Next == null)
                        {
                            sectionNext = Data.CurrentRace.GetSectionData(Track.Sections.First.Value);
                        }
                        else
                        {
                            sectionNext = Data.CurrentRace.GetSectionData(linkedListNode.Next.Value);
                        }
                        if(sectionNext.Left == null || sectionNext.Right == null)
                        {
                            if(linkedListNode.Value.SectionType == SectionTypes.Finish)
                            {
                                sectiondata.Left.Laps += 1;
                                if(sectiondata.Left.Laps == MaxLaps)
                                {
                                    sectiondata.Left = null;
                                }
                            }
                        }
                        if (sectionNext.Left == null)
                        {
                            sectionNext.Left = sectiondata.Left;
                            sectiondata.Left = null;
                            sectiondata.DistanceLeft = 0;
                        }
                        else if (sectionNext.Right == null)
                        {
                            sectionNext.Right = sectiondata.Left;
                            sectiondata.Left = null;
                            sectiondata.DistanceLeft = 0;
                        }
                    }

                }
                if (sectiondata.Right != null)
                {
                    int distance = sectiondata.Right.Equipment.Speed * sectiondata.Right.Equipment.Quality;
                    sectiondata.DistanceRight += distance;
                    if ((sectiondata.DistanceRight) > Lengte)
                    {
                        if (linkedListNode.Next == null)
                        {
                            sectionNext = Data.CurrentRace.GetSectionData(Track.Sections.First.Value);
                        }
                        else
                        {
                            sectionNext = Data.CurrentRace.GetSectionData(linkedListNode.Next.Value);
                        }
                        if (sectionNext.Left == null || sectionNext.Right == null)
                        {
                            if (linkedListNode.Value.SectionType == SectionTypes.Finish)
                            {
                                sectiondata.Right.Laps += 1;
                                if (sectiondata.Right.Laps == MaxLaps)
                                {
                                    sectiondata.Right = null;
                                }
                            }
                        }
                        if (sectionNext.Right == null)
                        {
                            sectionNext.Right = sectiondata.Right;
                            sectiondata.Right = null;
                            sectiondata.DistanceRight = 0;
                        }
                        else if (sectionNext.Left == null)
                        {
                            sectionNext.Left = sectiondata.Right;
                            sectiondata.Right = null;
                            sectiondata.DistanceRight = 0;
                        }
                    }
                }
                linkedListNode = linkedListNode.Previous;

            }
            OnDriversChanged();
        }
        public void Start()
        {
            _timer.Start();
        }

        public void CleanUp()
        {
             
        }
        public virtual void OnDriversChanged()
        {
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }

    }
}
