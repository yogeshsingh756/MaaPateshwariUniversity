using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.DTOs
{
    public class CourseDto
    {
        public long CourseId { get; set; }
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public decimal Credit { get; set; }
        public string Component { get; set; } = "";
    }

    public class SemesterDto
    {
        public byte SemesterId { get; set; }
        public string SemesterName { get; set; } = "";
        public List<CourseDto> Courses { get; set; } = new();
    }

    public class ProgramSyllabusDto
    {
        public long ProgramId { get; set; }
        public string ProgramName { get; set; } = "";
        public string DegreeLevel { get; set; } = "";
        public byte DurationYears { get; set; }
        public int? TotalCredits { get; set; }
        public List<SemesterDto> Semesters { get; set; } = new();
    }
}
