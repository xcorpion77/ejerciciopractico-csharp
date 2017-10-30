using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectopractico1
{
    public class Ranking
    {
        #region Members
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Score> score;
        public List<Score> Score
        {
            get { return score; }
        }

        public Ranking(string name, List<Score> score)
        {
            this.name = name;
            this.score = score;
        }

        //public Ranking(string data)
        //{
        //    string[] splittedData = data.Split('-');
        //    this.name = splittedData[0];
        //    this.score = (List)int.Parse(splittedData[1]);
        //}

        public override string ToString()
        {
            return string.Format("Ranking: {0}", Name, Score);
        }

        #endregion
    }
}
