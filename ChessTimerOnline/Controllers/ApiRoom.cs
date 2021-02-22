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

        // GET: api/<ApiRoom>/GetRoom/5
        [HttpGet("{id}")]
        public Room GetRoom(int id)
        {
            return GetRoomFromList(id);
        }

        // POST api/<ApiRoom>/
        [HttpPost]
        public void AddRoom(Room room)
        {
            ListRooms.Add(room);
        }

        // PUT api/<ApiRoom>/1
        [HttpPut("{id}")]
        public void ChangeTimeUpdate(int id, Room room)
        {
            Room roomFind = GetRoomFromList(id);
            int index = ListRooms.IndexOf(roomFind);
            ListRooms[index].TimePlayer1 = room.TimePlayer1;
        }

        // DELETE api/<ApiRoom>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Room room = GetRoomFromList(id);
            ListRooms.Remove(room);
        }
    }
}
