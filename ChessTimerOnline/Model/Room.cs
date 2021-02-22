using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTimerOnline.Model
{
    public class Room
    {
        /// <summary>
        /// For time use seconds
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Player1 { get; set; }
        public int TimePlayer1 { get; set; }
        public string Player2 { get; set; }
        public int TimePlayer2 { get; set; }
        public int AddTime { get; set; }

    }
}
