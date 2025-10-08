using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApi.Models
{
    public class Student
    {
         [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // khoá ngoại tham chiếu đến Class
        [ForeignKey("Class")]
        public int ClassId { get; set; }

        // thuộc tính điều hướng
        public Class Class { get; set; }
    }
}