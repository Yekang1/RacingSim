using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using static Model.Section;

namespace RacingSim
{
    public static class Visualisatie
    {
        #region graphics

        private static string[] _finishHorizontal =
        {
            "----",
            "  2#",
            " 1 #",
            "----"
        };

        private static string[] _finishVertical = {
            "|  |",
            "|1 |",
            "| 2|",
            "|##|"
        };

        public static string[] _rechtHorizontaal =
        {
            "----",
            "  2 ",
            " 1  ",
            "----"
        };

        private static string[] _bochtRechtsBoven =
        {
            @"--\ ",
            @" 2 \",
            @"1  |",
            @"\  |"

        };

        private static string[] _rechtVerticaal =
        {
            "|  |",
            "|1 |",
            "| 2|",
            "|  |"
        };
        private static string[] _startDeelnemerHorizontaal =
        {
            "----",
            "  2>",
            " 1> ",
            "----"
        };

        private static string[] _bochtLinksBoven =
        {
            @" /--",
            @"/ 2 ",
            @"|1  ",
             "|  /"

        };
        private static string[] _bochtLinksOnder =
        {
            @"|  \",
            @"|  2",
            @"\ 1 ",
            @" \--"

        };
        private static string[] _bochtRechtsOnder =
        {
            @"/  |",
            @" 1 |",
            @"2  /",
             "--/ "

        };
        private static string[] _startVerticaal =
        {
            "|^ |",
            "|2^|",
            "| 1|",
            "|  |"
        };
        #endregion

        public static int x = 0;
        public static int y = 0;
        public static void Initalize()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
        public static void Drawtrack(Track track)
        {
            x = 0;
            y = 0;
            string richting = "Noord";
            Console.SetCursorPosition(x, y);
            foreach (Section tracks in track.Sections)
            {   
                SectionData sectiontdata = Data.CurrentRace.GetSectionData(tracks);
                if (tracks.SectionType.Equals(Section.SectionTypes.RightCorner))
                {
                    if (richting == "Noord")
                    {
                        InBeeld(_bochtLinksBoven, sectiontdata);
                        richting = ("Oost");
                    }else if(richting == "Oost")
                    {
                        InBeeld(_bochtRechtsBoven, sectiontdata);
                        richting = ("Zuid");
                    }else if(richting == "Zuid")
                    {
                        InBeeld(_bochtRechtsOnder, sectiontdata);
                        richting = ("West");
                    }
                    else if (richting == "West")
                    {
                        InBeeld(_bochtLinksOnder, sectiontdata);
                        richting = ("Noord");
                    }
                }
                else if ((tracks.SectionType.Equals(Section.SectionTypes.LeftCorner)))
                {
                    if (richting == "Noord")
                    {
                        InBeeld(_bochtRechtsBoven, sectiontdata);
                        richting = ("West");
                    }
                    else if (richting == "West")
                    {
                        InBeeld(_bochtLinksBoven, sectiontdata);
                        richting = ("Zuid");
                    }
                    else if (richting == "Zuid")
                    {
                        InBeeld(_bochtLinksOnder, sectiontdata);
                        richting = ("Oost");
                    }
                    else if(richting == "Oost")
                    {
                        InBeeld(_bochtRechtsOnder, sectiontdata);
                        richting = ("Noord");
                    }
                }
                else if ((tracks.SectionType.Equals(Section.SectionTypes.Straight)))
                {
                    if (richting == "West" || richting == "Oost")
                    {
                        InBeeld(_rechtHorizontaal, sectiontdata);
                    }
                    else if(richting == "Noord" || richting == "Zuid")
                    {
                        InBeeld(_rechtVerticaal, sectiontdata);
                    }
                }
                else if ((tracks.SectionType.Equals(Section.SectionTypes.Finish)))
                {
                    if (richting == "West" || richting == "Oost")
                    {
                        InBeeld(_finishHorizontal, sectiontdata);
                    }
                    else if (richting == "Noord" || richting == "Zuid")
                    {
                        InBeeld(_finishVertical, sectiontdata);
                    }
                }
                else if ((tracks.SectionType.Equals(Section.SectionTypes.StartGrid)))
                {
                    if (richting == "West" || richting == "Oost")
                    {
                        InBeeld(_startDeelnemerHorizontaal, sectiontdata);
                    }
                    else if (richting == "Noord" || richting == "Zuid")
                    {
                        InBeeld(_startVerticaal, sectiontdata);
                    }
                }
                if(richting == "Oost")
                {
                    if (y >= 4 && x >= 0)
                    {
                        x = x + 4;
                        y = y - 4;
                    }
                    Console.SetCursorPosition(x, y);
                }
                else if(richting == "West")
                {
                    if (y >= 4 && x >= 4)
                    {
                        x = x - 4;
                        y = y - 4;
                    }
                    Console.SetCursorPosition(x, y);
                }
                else if (richting == "Noord")
                {

                    if (y >= 8 && x >= 0)
                    {
                        y = y - 8;
                    }
                    Console.SetCursorPosition(x, y);
                }

            }

        }

        public static void InBeeld(string[] sectie, SectionData sectiondata)
        {
            foreach (string section in sectie)
            { 
                Console.Write(LaatDeelnemerZien(section, sectiondata.Left, sectiondata.Right));
                y++;
                Console.SetCursorPosition(x, y);
            }
        }

        public static string LaatDeelnemerZien(string s, IParticipant participant1, IParticipant participant2)
        {
            if (participant1 != null)
            {
                s = s.Replace('1', participant1.Name[0]);
            }
            else
            {
                s = s.Replace('1', ' ');
            }
            if (participant2 != null)
            {
                s = s.Replace('2', participant2.Name[0]);
            }
            else
            {
                s = s.Replace('2', ' ');
            }
            return s;
        }

        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Drawtrack(e.Track);
        }


        
    }
}
