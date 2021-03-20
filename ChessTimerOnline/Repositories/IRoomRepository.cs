using System.Collections.Generic;
using System.Threading.Tasks;
using ChessTimerOnline.Model;

namespace ChessTimerOnline.Repositories
{
    
    /// <summary>
    /// Интерфейс для доступа к базе комнат.
    /// Используем здесь асинхронные сигнатуры для возможной
    /// расширяемости и подключения реальной базы данных 
    /// </summary>
    public interface IRoomRepository
    {
        Task<int> AddRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int id);
        Task<Room> GetRoomByIdOrNullAsync(int id);
        Task<IReadOnlyList<Room>> GetRoomsAsync();
    }
}