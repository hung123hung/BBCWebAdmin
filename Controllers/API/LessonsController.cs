using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBCWebAPI.Models;
using BBCWebAPI.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BBCWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController: ControllerBase
    {
        private DataContext _context;
        public LessonsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public IEnumerable<Lesson> GetLessons()
        {
            try
            {
                return _context.Lessons;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
