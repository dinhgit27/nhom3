using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Controllers
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
    }
}
[HttpPost]
public async Task<IActionResult> CreateClass(Class newClass)
{
    _context.Classes.Add(newClass);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetAllClasses), new { id = newClass.Id }, newClass);//tra ve thong tin class vua them trong http 201
    //await _context.SaveChangesAsync(): Lưu các thay đổi vào cơ sở dữ liệu một cách không đồng bộ

}
[HttpGet]
public async Task<IActionResult> GetAllClasses()
{
    var classes = await _context.Classes.FindAsync(id);
    f (classes == null) return NotFound();
    return classes;// tra ve danh sach class trong http 200
    //_context.Classes: truy vấn bảng Classes trong cơ sở dữ liệu
    //findAsync(id): tìm kiếm lớp học theo id từ trong database
    // Trả về danh sách tất cả các lớp Không đồng bộ sinh viên  

}

[HttpGet]
public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByClass(int classId)
{
return await _context.Students.Where(s => s.ClassId == classId).ToListAsync();
}// Trả về danh sách tất cả các sinh viên trong một lớp cụ thể
