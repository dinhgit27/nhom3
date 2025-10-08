using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Models;

namespace nhom3moi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // POST: api/classes
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass([FromBody] Class classObj)
        {
            if (classObj == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Classes.Add(classObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClass), new { id = classObj.Id }, classObj);
        }

        // GET: api/classes/{id} (dùng cho CreatedAtAction)
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var classObj = await _context.Classes.FindAsync(id);
            if (classObj == null)
            {
                return NotFound();
            }
            return classObj;
        }
    }
}