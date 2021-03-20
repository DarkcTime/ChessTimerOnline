

using System.Collections.Generic;
using System.Threading.Tasks;
using ChessTimerOnline.Model;

namespace ChessTimerOnline.Repositories
{
    public interface IGameRepository
    {
        public Task<Game> AcceptUserAsync(int roomId, string playerName);
        public Task<IReadOnlyList<Game>> GetGamesAsync();

        public Task<Game> GetGameByRoomIdOrNullAsync(int roomId);
    }
}