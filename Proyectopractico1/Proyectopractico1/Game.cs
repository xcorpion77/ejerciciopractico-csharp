using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectopractico1
{
    class Game
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        private Genres genre;
        public Genres Genres
        {
            get { return genre; }
        }

        private List<Platforms> platform;
        public List<Platforms> Platforms
        {
            get { return platform; }
        }

         private int releasedate;
        public int Release_Date
        {
            get { return releasedate; }  
        }

        private Dictionary<Platforms, Ranking> ranking;
        public Dictionary<Platforms, Ranking> Ranking
        {
            get { return ranking; }
        }

        public Game(string name, Genres genre, List<Platforms> platform, int releasedate, Dictionary <Platforms, Ranking> ranking)
        {
            this.name = name;
            this.genre = genre;
            this.platform = platform;
            this.releasedate = releasedate;
            this.ranking = ranking;
        }


        //public Game(string data)
        //{
        //    string[] splittedData = data.Split('-');
        //    this.name = splittedData[0];
        //    this.genre = (Genres)int.Parse(splittedData[1]);
        //    this.platform = (List)int.Parse(splittedData[2]);
        //    this.releasedate = int.Parse(splittedData[3]);
        //    this.ranking = (Dictionary)int.Parse(splittedData[4]);
        //}

        public override bool Equals(object obj)
        {
            if (obj is Game)
            {
                Game aux = (Game)obj;
                return this.Name == aux.Name;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("---{0} ({3})- {2} - {1}---\nRankings: \n{4} ({})", Name, Release_Date, Platforms, Genres);
        }

    }
}
