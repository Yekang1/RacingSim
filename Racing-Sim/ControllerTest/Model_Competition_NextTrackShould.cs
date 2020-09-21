using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("UNITTEST");
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();

            
            Assert.AreEqual(track, result);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("UNITTEST");
            _competition.Tracks.Enqueue(track);
            
            for (int i = 0; i < 2; i++)
            {
                var result = _competition.NextTrack();
                if(i == 1)
                {
                    Assert.IsNull(result);
                }
            }
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track1 = new Track("UNITTEST");
            Track track2 = new Track("TRACKTEST");
            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            for (int i = 0; i < 2; i++)
            {
                var result = _competition.NextTrack();
                if (i == 1)
                {
                    Assert.AreEqual(result, track2);
                }
            }
            
        }
    }
    
  
}
