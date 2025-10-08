using System;

namespace StudentManagementApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public Class? Class { get; set; }  // Nullable để tránh warning
    }
}