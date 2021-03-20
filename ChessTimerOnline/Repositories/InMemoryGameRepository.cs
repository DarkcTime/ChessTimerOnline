using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessTimerOnline.Model;

namespace ChessTimerOnline.Repositories
{
    public class InMemoryGameRepository: IGameRepository
    {
        private readonly List<Game> _games = new List<Game>();
        private readonly IRoomRepository _roomRepository;

        public InMemoryGameRepository(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Game> AcceptUserAsync(int roomId, string playerName)
        {
            Game existingGame = null;

            lock (_games)
            {

                existingGame = _games
                    .FirstOrDefault(g => g.Room.Id == roomId);
            }

            if (existingGame == null)
            {
                Room room = await _roomRepository
                    .GetRoomByIdOrNullAsync(roomId);

                if (room == null)
                {
                    throw new ArgumentException("No room for given roomId");
                }
                
                existingGame = new Game()
                {
                    Room = room,
                    RemainingTimePlayer1 =
                        TimeSpan.FromSeconds(room.TimePlayer1),
                    RemainingTimePlayer2 =
                        TimeSpan.FromSeconds(room.TimePlayer2)
                };
                
                lock (_games)
                {
                    _games.Add(existingGame);
                }
            }

            if (existingGame.Room.Player1 == playerName)
            {
                existingGame.IsPlayer1Accepted = true;
            }

            if (existingGame.Room.Player2 == playerName)
            {
                existingGame.IsPlayer2Accepted = true;
            }
            

            return existingGame;
        }
    

        public Task<IReadOnlyList<Game>> GetGamesAsync()
        {
            lock (_games)
            {
                return Task.FromResult((IReadOnlyList<Game>)_games);
            }
        }

        public Task<Game> GetGameByRoomIdOrNullAsync(int roomId)
        {
            lock (_games)
            {
                return Task.FromResult(_games
                    .FirstOrDefault(g => g.Room.Id == roomId));
            }
            
        }
    }
}