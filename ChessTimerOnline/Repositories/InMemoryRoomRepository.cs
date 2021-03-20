using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessTimerOnline.Model;

namespace ChessTimerOnline.Repositories
{
    
    /// <summary>
    /// Репозиторий комнат используюший
    /// оперативную память для хранения данных
    /// </summary>
    public class InMemoryRoomRepository: IRoomRepository
    {
        private readonly List<Room> _rooms = new List<Room>();

        public Task<int> AddRoomAsync(Room room)
        {
            lock (_rooms)
            {       
                room.Id = _rooms.Count + 1;
                _rooms.Add(room);
                return Task.FromResult(room.Id);
            }
        }

        public Task<bool> DeleteRoomAsync(Room room)
        {
            lock (_rooms)
            {
                for (int i = 0; i < _rooms.Count; i++)
                {
                    if (_rooms[i].Id == room.Id)
                    {
                        _rooms.RemoveAt(i);
                        return Task.FromResult(true);
                    }
                }

                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteRoomAsync(int id)
        {
            lock (_rooms)
            {
                for (int i = 0; i < _rooms.Count; i++)
                {
                    if (_rooms[i].Id == id)
                    {
                        _rooms.RemoveAt(i);
                        return Task.FromResult(true);
                    }
                }

                return Task.FromResult(false);
            }
        }

        public Task<Room> GetRoomByIdOrNullAsync(int id)
        {
            lock (_rooms)
            {
                return Task.FromResult(_rooms.FirstOrDefault(
                    r => r.Id == id));
            }
        }

        public Task<IReadOnlyList<Room>> GetRoomsAsync()
        {
            lock (_rooms)
            {
                return Task.FromResult((IReadOnlyList<Room>) _rooms);
            }
        }
    }
}