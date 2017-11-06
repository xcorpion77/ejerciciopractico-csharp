using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Proyectopractico1
{
    public static class GameServices
    {

        private static List<Player> players = new List<Player>();
        public static List<Player> Players
        {
            get { return players; }
        }

        private static List<Game> games = new List<Game>();
        public static List<Game> Games
        {
            get { return games; }
        }

        //Aquí empieza la segunda parte del proyecto.

        public static string PlayersToString()
        {
            string data = "";
            foreach(Player p in players)
            {
                string playerData = p.Nickname + "-" + p.Email + "-" + p.Country + "\n";
                data += playerData;

            }
            return data;
        }

        public static string GamesToString()
        {
            string data = "";
            foreach (Game g in games)
            {
                string gameData = "";
                gameData = g.Name + "-" + g.Genre + "-" + g.ReleaseDate + "-";
                foreach(Platforms p in g.PlatForms)
                {
                    gameData += p + ", ";
                }
                gameData = gameData.TrimEnd(' ', ',');
                data += gameData + "\n";
            }
            return data;
        }

        public static string RankingsToString()
        {
            string data = "";

            foreach(Game g in games)
            {
                string rankingdata = g.Name + "-";

                foreach (Ranking r in g.Rankings.Values)
                {
                    rankingdata += r.Name + "-";
                    IEnumerable<Score> scoreSorted = r.Scores.OrderByDescending(score => score.Points);
                    foreach(Score s in scoreSorted)
                    {
                        rankingdata += s.NickName + "=" + s.Points + ", ";
                    }
                    rankingdata = rankingdata.TrimEnd(' ', ',');
                    rankingdata = rankingdata + "|";
                }
                data += rankingdata + "\n"; 
            }
            
            return data;
        }
        
//Con Export se creará un archivo de la siguiente estructura:
        
        public static void Export()
        {
            //Convertir los objetos Jugadores en string
            string playerData = PlayersToString();
            //Convertir los objetos Juegos en string
            string gameData = GamesToString();
            //Convertir los objetos Ranking en string
            string rankingData = RankingsToString();

            //Exportar strings a data.txt
            try
            {
                StreamWriter file = File.CreateText("../../Data/data.txt");
                string completeData = playerData + "\n*+*+*+*\n" + gameData + "\n*+*+*+*\n" + rankingData;
                file.Write(completeData);
                file.Close();
                Console.WriteLine("The data has been exported without issues");
            }
            catch (Exception e)
            {

                Console.WriteLine("Error creating the file data.txt: " + e);
            }
        }

        //Con Import se leerá el archivo que se ha creado.
        
        public static List<string> ReadFile(string path)
        {
            List<string> lines = new List<string>();
            try
            {
                StreamReader file = File.OpenText("../../Data/data.txt");
                string line = "";
                while (line != null)
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        lines.Add(line);
                    }
                }
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file couldnt be readed: " + e);
            }
            return lines;
        }


        public static void Import()
        {
            //Leemos las lineas del archivo
            List<string> lines = ReadFile("../../Data/data.txt");
            //Separamos cada linea que se refiera a cada elemento, que son: Player, Game y Ranking
            List<string> playerLines = new List<string>();
            List<string> gameLines = new List<string>();
            List<string> rankingLines = new List<string>();
            //Añadimos las lineas a cada lista dependiendo de lo que son
            bool isRanking = false;
            bool isGame = false;

            foreach (string line in lines)
            {
                if (isRanking == true)
                {
                    rankingLines.Add(line);
                }

                if (line == "*+*+*+*" && isGame == false)
                {
                    isGame = true;
                }
                else if (line == "")
                {
                    //Linea sin datos, asi que no nos interesa usarla para nada
                }else
                {
                    if (!isGame)
                    {
                        playerLines.Add(line);
                    }else
                    {
                        gameLines.Add(line);
                    }

                    if (line == "*+*+*+*" && isRanking == false)
                    {
                        isRanking = true;
                    }
                }
            }
            //Split a las listas de cada elemento
            players = new List<Player>();
            foreach(string line in playerLines)
            {
                Player p = new Player(line);
                players.Add(p);
            }

            games = new List<Game>();
            foreach (string line in gameLines)
            {
                foreach(string line2 in rankingLines)
                {
                    Ranking r = new Ranking(line2);
                    Game g = new Game(line, r);
                }
            }
        }        

        
        //Aquí empieza la tercera parte:
        
        //a) Cuál es el juego más antiguo de la empresa:
        
        public static Game OldestGame()
        {
            Game game = null;
            foreach (Game g in games)
            {
                if (game == null)
                {
                    game = g;
                }

                if (g.ReleaseDate < game.ReleaseDate)
                {
                    game = g;
                }
            }
            return game;
        }

        //b) Ranking de un determinado juego si la info que se introduce
        //para la consulta es el nombre del juego y el ranking.
        public static int PointsCountByName(string gameName, string rankingName)
        {
            int count = 0;
            foreach(Game g in games)
            {
                if(g.Name == gameName) { 
                    foreach (Ranking r in g.Rankings.Values)
                    {
                        if(r.Name == rankingName)
                        { 
                            count += r.Scores.Count;
                        }
                    }
                }
            }
            return count;
        }

        //c) Cuántos juegos de un determinado género existen publicados.
        
        public static int CountByGenre(Genres genre)
        {
            int count = 0;
            foreach (Game g in games)
            {
                if (genre == g.Genre)
                {
                    count++;
                }
            }
            return count;
        }

        //d) Cuál es el juego que tiene más puntuaciones registradas.
        public static Game GameByPointsCount()
        {
            Game game = null;
            foreach(Game g in games)
            {
                if (game == null)
                {
                    game = g;
                }

                if (game.Rankings.Count < g.Rankings.Count)
                {
                    game = g;
                }
            }
            return game;
        }

        //e) ¿Existe algún juego con la palabra "Call" contenida en su nombre?
        public static bool AnyGameWithCallOnItsName()
        {
            bool res = false;
            foreach (Game g in games)
            {
                if (g.Name.Contains("Call"))
                {
                    res = true;
                }
            }
            return res;
        }

        //f)¿Cuáles son los juegos a los que un determinado jugador ha jugado?
        public static void GamesPlayedBySomeone(string nickname)
        {
            Console.WriteLine("The player has played: ");
            foreach (Game g in games)
            {
                foreach(Ranking r in g.Rankings.Values)
                {
                   foreach (Score s in r.Scores)
                    {
                        foreach (Platforms p in g.PlatForms)
                        {
                            if (s.NickName == nickname)
                            {
                                Console.WriteLine(g.Name);
                            }
                            break;
                        }                       
                    }
                    break;
                }
            }
            
        }

        //g)¿A qué juegos ha jugado cada jugador?
        public static void HowManyGamesHavePlayedEveryone()
        {
            foreach(Player p in players)
            {
                Console.WriteLine("-> " + p.Nickname);
                foreach (Game g in games)
                {
                    foreach (Ranking r in g.Rankings.Values)
                    {
                        foreach(Score s in r.Scores)
                        {
                            foreach (Platforms p2 in g.PlatForms)
                            {
                                if (s.NickName == p.Nickname)
                                {
                                    Console.WriteLine(g.Name);
                                }
                                break;
                            }                            
                        }
                        break;
                    }
                }
            }
            
        }
    }
}
