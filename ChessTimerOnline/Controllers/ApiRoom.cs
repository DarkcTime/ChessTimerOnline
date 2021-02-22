using ChessTimerOnline.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessTimerOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRoom : ControllerBase
    {
        public static List<Room> ListRooms = new List<Room>();

        private Room GetRoomFromList(int id)
        {
            return ListRooms.Where(i => i.Id == id).FirstOrDefault();
        }

        // GET: api/<ApiRoom>/
        [HttpGet]
        public List<Room> GetRooms()
        {
            return ListRooms; 
        }

        // GET: api/<ApiRoom>/1
        [HttpGet("{id:int}")]
        public ActionResult<Room> GetRoom(int id)
        {
            try
            {
                Room room = GetRoomFromList(id); 
                if(room == null)
                {
                    return NotFound("Room not found");
                }
                return room;
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message.ToString());
            }
           
        }

        // POST api/<ApiRoom>/
        [HttpPost]
        public ActionResult<string> AddRoom(Room room)
        {
            try
            {
                if (room == null)
                {
                    return BadRequest("Room is null");
                }

                ListRooms.Add(room);

                return "Success"; 
            }
            catch(Exception ex)
            {

                return NotFound(ex.Message.ToString());
            }
            
        }

        // PUT api/<ApiRoom>/1
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
        }

        // DELETE api/<ApiRoom>/1
        [HttpDelete("{id:int}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                Room room = GetRoomFromList(id);
                if(room == null)
                {
                    return NotFound("Room not found");
                }
                ListRooms.Remove(room); 

                return "Success"; 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message.ToString());
            }
                    
        }
    }
}
