using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace ChessTimerOnline.Repositories
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection collection)
        {
            collection.AddRoomRepository();
            collection.AddGamesRepository();
        } 
        public static void AddRoomRepository(this IServiceCollection collection)
        {
            collection.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
        }
        
        public static void AddGamesRepository(this IServiceCollection collection)
        {
            collection.AddSingleton<IGameRepository, InMemoryGameRepository>();
        }
    }
}