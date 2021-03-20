using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChessTimerOnline.Model;
using ChessTimerOnline.Repositories;
using Microsoft.AspNetCore.SignalR;


#pragma warning disable 4014

namespace ChessTimerOnline.Hubs
{
    public class TimerHub: Hub
    {
        private static string failureMethodName = "failure";
        
        private readonly IRoomRepository _roomRepository;
        private readonly IGameRepository _gameRepository;

        private readonly Dictionary<int, List<string>> _roomConnectionIds = new();
        
        public TimerHub(IRoomRepository roomRepository, IGameRepository gameRepository)
        {
            _roomRepository = roomRepository;
            _gameRepository = gameRepository;

            UpdateTimerAsync();
        }

        private async Task UpdateTimerAsync()
        {
            var delay = TimeSpan.FromSeconds(1);
            
            while (true)
            {
                foreach (var game in await _gameRepository.GetGamesAsync())
                {
                    if (game.IsGameRunning)
                    {
                        if (game.PlayerIdCurrentMove == 1)
                        {
                            game.RemainingTimePlayer1 =
                                game.RemainingTimePlayer1.Subtract(delay);
                        }
                        else if (game.PlayerIdCurrentMove == 2)
                        {
                            game.RemainingTimePlayer2 =
                                game.RemainingTimePlayer2.Subtract(delay);
                        }
                    }

                    List<string> connections = null;

                    lock (_roomConnectionIds)
                    {
                        connections = _roomConnectionIds[game.Room.Id];
                    }

                    if (connections != null)
                    {
                        
                        //Fire and forget
                        Clients.Clients(connections).SendAsync(
                            "updateTime",
                            game.RemainingTimePlayer1.Seconds,
                            game.RemainingTimePlayer2.Seconds
                        );
                        
                    }
                    
                }

                await Task.Delay(delay);
            }
        }

        public async Task GetInfo(int roomId)
        {
            try
            {
                var game = await
                    _gameRepository.GetGameByRoomIdOrNullAsync(roomId);


                if (game == null)
                {
                    Clients.Caller.SendAsync("usersAccepted",
                        false,
                        false
                    );
                }
                else
                {
                    Clients.Caller.SendAsync("usersAccepted",
                        game.IsPlayer1Accepted,
                        game.IsPlayer2Accepted
                    );
                }

            }
            catch (Exception e)
            {
                SendFailureToCallerAsync(e.Message);
            }
        }

        private Task SendFailureToCallerAsync(string message)
        {
            return Clients.Caller.SendAsync(failureMethodName, message);
        } 
        
        public async Task SendInit(int roomId, string playerName)
        {
            try
            {
                Game game = await _gameRepository.AcceptUserAsync(roomId, playerName);

                lock (_roomConnectionIds)
                {

                    List<string> connections;
                    
                    if (_roomConnectionIds.ContainsKey(roomId))
                    {
                        connections = _roomConnectionIds[roomId];
                    }
                    else
                    {
                        connections = new List<string>(2);
                        _roomConnectionIds[roomId] = connections;
                    }
                    
                    connections.Add(Context.ConnectionId);
                }
                
                Clients.Caller.SendAsync("usersAccepted", 
                    game.IsPlayer1Accepted, 
                    game.IsPlayer2Accepted);
                
            }
            catch (Exception ex)
            {
                SendFailureToCallerAsync(ex.Message);
            }
        }
        
        
    }
}