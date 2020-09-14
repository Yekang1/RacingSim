﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class Competition
    {
       
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Participants = participants;
            Tracks = tracks;
        }
        public Track NextTrack()
        {
            return null;
        }

    }
}
