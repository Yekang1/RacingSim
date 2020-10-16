using System;
using System.Threading;
using Controller;
using Model;
using static Model.Section;

namespace RacingSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Visualisatie.Initalize();
            Visualisatie.Drawtrack(Data.CurrentRace.Track);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
