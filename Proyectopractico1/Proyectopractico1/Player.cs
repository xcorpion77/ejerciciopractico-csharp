using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectopractico1
{
    public class Player
    {
            #region Members
            private string nickname;
            public string Nickname
            {
                get { return nickname; }
            }

            private string email;
            public string Email
            {
                get { return email; }
                set { email = value; }
            }

            private Countries country;
            public Countries Countries
            {
                get { return country; }
                set { country = value; }
            }
            


        #endregion
        public Player(string nickname, string email, Countries country)
        {
            this.nickname = nickname;
            this.email = email;
            this.country = 0;
        }

        
        //public Player(string data)
        //{
        //    string[] splittedData = data.Split('-');
        //    this.nickname = splittedData[0];
        //    this.email = splittedData[1];
        //    this.country = (Countries)int.Parse(splittedData[2]);
        //}

        public override bool Equals(object obj)
        {
            if (obj is Player)
            {
                Player aux = (Player)obj;
                return this.Nickname == aux.Nickname;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Nickname, Email, Countries);
        }

    }
}


