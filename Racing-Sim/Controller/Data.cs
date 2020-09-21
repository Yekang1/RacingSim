using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Model;


namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            DeelnemerToeVoegen();
            TrackToeVoegen();
        }

        public static void DeelnemerToeVoegen()
        {
            IParticipant driver1 = new Driver("Test");
            IParticipant driver2 = new Driver("bob");
            IParticipant driver3 = new Driver("top");

            Competition.Participants.Add(driver1);
            Competition.Participants.Add(driver2);
            Competition.Participants.Add(driver3);
        }

        public static void TrackToeVoegen()
        {
            Track track1 = new Track("coolTrack");
            Track track2 = new Track("AWEASOMEA");
            Track track3 = new Track("TRACKMANIA");

            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
            Competition.Tracks.Enqueue(track3);
        }
        public static void NextRace()
        {
            var race = Competition.NextTrack();
            if (race != null)
            {
                CurrentRace = new Race(race, Competition.Participants);
            }
        }
    }
}
