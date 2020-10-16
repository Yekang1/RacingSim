using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;
        private Track _track;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _track = new Track("Test");
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

        [Test]
        public void ArrayToLinkedList_ReturnIsNotNull()
        {
            SectionTypes[] sectie = new SectionTypes[] { SectionTypes.Finish};
            var result = _track.ArrayToLinkedList(sectie);
            Assert.IsNotNull(result);
            
        }
    }
    
  
}
