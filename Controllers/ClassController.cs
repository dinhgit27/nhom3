[HttpGet]
public async Task<IActionResult> GetAllClasses()
{
    var classes = await _context.Classes.ToListAsync();
    return Ok(classes);// tra ve danh sach class trong http 200
    //_context.Classes: truy vấn bảng Classes trong cơ sở dữ liệu
    //ToListAsync(): chuyển đổi kết quả truy vấn thành danh sách class không đồng bộ
    // Trả về danh sách tất cả các lớp Không đồng bộ sinh viên  

}
[HttpPost]
public async Task<IActionResult> CreateClass(Class newClass)
{
    _context.Classes.Add(newClass);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetAllClasses), new { id = newClass.Id }, newClass);//tra ve thong tin class vua tao trong http 201
    //await _context.SaveChangesAsync(): Lưu các thay đổi vào cơ sở dữ liệu một cách không đồng bộ

}
