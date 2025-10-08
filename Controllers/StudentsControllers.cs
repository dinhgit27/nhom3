using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhom3moi.Models;

namespace nhom3moi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.Include(s => s.Class).ToListAsync();
            return Ok(students);
        }

        // GET /api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Class).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST /api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student newStudent)
        {
            if (newStudent == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Classes.AnyAsync(c => c.Id == newStudent.ClassId))
            {
                return BadRequest("ClassId does not exist.");
            }

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }

        // GET /api/classes/{classId}/students
        [HttpGet("classes/{classId}/students")]
        public async Task<IActionResult> GetStudentsByClass(int classId)
        {
            if (!await _context.Classes.AnyAsync(c => c.Id == classId))
            {
                return NotFound("Class not found.");
            }

            var students = await _context.Students.Where(s => s.ClassId == classId).ToListAsync();
            return Ok(students);
        }

        // PUT /api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid student data.");
            }

            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
            existingStudent.ClassId = existingStudent.ClassId; // Không cho phép đổi ClassId

            _context.Entry(existingStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}