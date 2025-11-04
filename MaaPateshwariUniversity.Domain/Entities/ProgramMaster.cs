using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Domain.Entities
{
    public class StreamMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short StreamId { get; set; }

        [Required, MaxLength(100)]
        public string StreamName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }

    public class DisciplineMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short DisciplineId { get; set; }

        [ForeignKey(nameof(StreamMaster))]
        public short StreamId { get; set; }

        [Required, MaxLength(150)]
        public string DisciplineName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }

    public class ProgramMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProgramId { get; set; }

        [Required, MaxLength(200)]
        public string ProgramName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string DegreeLevel { get; set; } = "UG"; // UG / PG / Diploma

        [ForeignKey(nameof(DisciplineMaster))]
        public short DisciplineId { get; set; }

        public byte DurationYears { get; set; } = 3;

        public int? TotalCredits { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CourseMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CourseId { get; set; }

        [Required, MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string CourseName { get; set; } = string.Empty;

        public decimal Credit { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class SemesterMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte SemesterId { get; set; }

        [Required, MaxLength(50)]
        public string SemesterName { get; set; } = string.Empty;
    }

    public class NEPComponentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ComponentTypeId { get; set; }

        [Required, MaxLength(30)]
        public string ComponentCode { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string ComponentName { get; set; } = string.Empty;
    }

    public class ProgramCourseMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProgramCourseId { get; set; }

        [ForeignKey(nameof(ProgramMaster))]
        public long ProgramId { get; set; }

        [ForeignKey(nameof(CourseMaster))]
        public long CourseId { get; set; }

        [ForeignKey(nameof(SemesterMaster))]
        public byte SemesterId { get; set; }

        [ForeignKey(nameof(NEPComponentType))]
        public byte ComponentTypeId { get; set; }

        public bool IsMandatory { get; set; } = true;
    }

    public class MajorMinorMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MajorMinorId { get; set; }

        public long MajorProgramId { get; set; }

        public long MinorProgramId { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class OrganizationProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrganizationProgramId { get; set; }

        [ForeignKey("Organization")]
        public long OrganizationId { get; set; }

        [ForeignKey("ProgramMaster")]
        public long ProgramId { get; set; }

        public int? IntakeCapacity { get; set; }

        [MaxLength(50)]
        public string? AffiliationStatus { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
