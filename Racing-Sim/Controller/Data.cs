using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Model;


namespace Controller
{
    static class Data
    {
        static Competition Competition { get; set; }

        static void Initialize(Competition competition)
        {
            competition = Competition;
        }
    }
}
