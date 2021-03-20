using ChessTimerOnline.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessTimerOnline.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessTimerOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRoom : ControllerBase
    {
        
        private readonly IRoomRepository _repository;

        public ApiRoom(IRoomRepository roomRepository)
        {
            _repository = roomRepository;
        }
        

        // GET: api/<ApiRoom>/
        [HttpGet]
        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _repository.GetRoomsAsync();
        }

        // GET: api/<ApiRoom>/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            try
            {
                Room room = await _repository.GetRoomByIdOrNullAsync(id); 
                
                if(room == null)
                {
                    return NotFound("Room not found");
                }
                
                return room;
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        // POST api/<ApiRoom>/
        [HttpPost]
        public async Task<ActionResult<AddRoomResult>> AddRoom(Room room)
        {
            try
            {
                if (room == null)
                {
                    return BadRequest("Room is null");
                }

                await _repository.AddRoomAsync(room);

                return new AddRoomResult()
                {
                    Id = room.Id
                };
                
            }
            catch(Exception ex)
            {

                return NotFound(ex.Message);
            }
            
        }

      /*  // PUT api/<ApiRoom>/1
        [HttpPut("{id:int}")]
        public ActionResult<Room> UpdateRoomTime(int id, Room room)
        {
            try
            {
                if(room == null)
                {
                    return BadRequest("Room is null");
                }

                if(id != room.Id)
                {
                    return BadRequest("Room ID mismatch");
                }

                Room roomToUpdate = GetRoomFromList(id); 

                if(roomToUpdate == null)
                {
                    return NotFound("Room not found");
                }

                int indexRoom = ListRooms.IndexOf(roomToUpdate);
                ListRooms[indexRoom].TimePlayer1 = room.TimePlayer1;
                ListRooms[indexRoom].TimePlayer2 = room.TimePlayer2;

                return ListRooms[indexRoom];
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message.ToString());  
            }
        }*/

        // DELETE api/<ApiRoom>/1
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool isSuccess = await _repository
                    .DeleteRoomAsync(id);

                if (isSuccess)
                {
                    return StatusCode(200);
                }
                else
                {
                    return NotFound("Room not found");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message.ToString());
            }
                    
        }
    }
}
