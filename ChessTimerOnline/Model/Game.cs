using System;

namespace ChessTimerOnline.Model
{
    public class Game
    {
        public Room Room { get; set; }
        public bool IsPlayer1Accepted { get; set; }
        public bool IsPlayer2Accepted { get; set; }
        public TimeSpan RemainingTimePlayer1 { get; set; }
        public TimeSpan RemainingTimePlayer2 { get; set; }

        /// <summary>
        /// By default player 1 starts the game
        /// </summary>
        public int PlayerIdCurrentMove { get; set; } = 1;
        
        public bool IsGameStopped { get; set; }
        
        public bool IsGameWaitingAccept =>
            !IsPlayer1Accepted ||
            !IsPlayer2Accepted;
        
        public bool IsGameRunning =>
            !IsGameStopped &&
            IsPlayer1Accepted &&
            IsPlayer2Accepted &&
            RemainingTimePlayer1.TotalSeconds > 0 &&
            RemainingTimePlayer2.TotalSeconds > 0;
        
        public bool IsGameFinished => 
            IsPlayer1Accepted &&
            IsPlayer2Accepted &&
            RemainingTimePlayer1.TotalSeconds == 0 &&
            RemainingTimePlayer2.TotalSeconds == 0;
    }
}