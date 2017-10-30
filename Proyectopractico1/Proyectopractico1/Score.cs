using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectopractico1
{
    public class Score
    {
        #region Members
        private string nickname;
        public string Nickname
        {
            get { return nickname; }
        }

        private int point;
        public int Points
        {
            get { return point; }
            set {
                if (value >= 0)
                {
                    point = value;
                }
            }
        }
#endregion
        public Score(string nickname, int point)
        {
            this.nickname = nickname;
            this.point = point;
        }


        //public Score(string data)
        //{
        //    string[] splittedData = data.Split('-');
        //    this.nickname = splittedData[0];
        //    this.point = int.Parse(splittedData[1]);
        //}

        public override string ToString()
        {
            return string.Format("Formato: \n{0} - {1}", Nickname, Points);
        }
    }
}
