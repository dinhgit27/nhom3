using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Models;

namespace StudentClassManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class nhom3 : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudents), new { id = newStudent.Id }, newStudent);
        }

        // GET /api/students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.Include(s => s.Class).ToListAsync();
            return Ok(students);
        }

        // GET /api/classes/{classId}/students
        [HttpGet("classes/{classId}/students")]
        public async Task<IActionResult> GetStudentsByClass(int classId)
        {
            var students = await _context.Students.Where(s => s.ClassId == classId).ToListAsync();
            return Ok(students);
        }

        // PUT /api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}