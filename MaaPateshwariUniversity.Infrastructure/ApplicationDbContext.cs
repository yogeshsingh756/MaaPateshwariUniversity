using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // ? Table Mappings
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<OrganizationType> OrganizationTypes => Set<OrganizationType>();
        public DbSet<InstitutionCategory> InstitutionCategories => Set<InstitutionCategory>();
        public DbSet<SubInstitutionType> SubInstitutionTypes => Set<SubInstitutionType>();
        public DbSet<ManagementType> ManagementTypes => Set<ManagementType>();
        public DbSet<GovernmentCategory> GovernmentCategories => Set<GovernmentCategory>();
        public DbSet<MinorityType> MinorityTypes => Set<MinorityType>();
        public DbSet<LocationType> LocationTypes => Set<LocationType>();

        // ? FIX: DbSet name should be plural (not Country)
        public DbSet<Country> Countries => Set<Country>(); // ? CHANGE

        public DbSet<State> States => Set<State>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<StatusMaster> StatusMasters => Set<StatusMaster>();
        public DbSet<OuCategory> OuCategories => Set<OuCategory>();
        public DbSet<AccreditationAgency> AccreditationAgencies => Set<AccreditationAgency>();
        public DbSet<OrganizationAccreditation> OrganizationAccreditations => Set<OrganizationAccreditation>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<StreamMaster> StreamMasters => Set<StreamMaster>();
        public DbSet<DisciplineMaster> DisciplineMasters => Set<DisciplineMaster>();
        public DbSet<ProgramMaster> ProgramMasters => Set<ProgramMaster>();
        public DbSet<CourseMaster> CourseMasters => Set<CourseMaster>();
        public DbSet<SemesterMaster> SemesterMasters => Set<SemesterMaster>();
        public DbSet<NEPComponentType> NEPComponentTypes => Set<NEPComponentType>();
        public DbSet<ProgramCourseMapping> ProgramCourseMappings => Set<ProgramCourseMapping>();
        public DbSet<MajorMinorMapping> MajorMinorMappings => Set<MajorMinorMapping>();
        public DbSet<OrganizationProgram> OrganizationPrograms => Set<OrganizationProgram>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

 
            b.Entity<Organization>()
                .HasKey(x => x.OrganizationId);

            b.Entity<Organization>()
                .Property(x => x.OrganizationId)
                .ValueGeneratedOnAdd();

            b.Entity<Organization>()
                .HasQueryFilter(x => !x.IsDeleted);


            b.Entity<District>()
                .HasIndex(x => new { x.StateId, x.DistrictName })
                .IsUnique();

      
            b.Entity<AccreditationAgency>()
                .HasIndex(x => x.AgencyCode)
                .IsUnique();

            b.Entity<State>()
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(s => s.CountryId)  
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "SuperAdmin" },
                new Role { RoleId = 2, Name = "UniversityAdmin" },
                new Role { RoleId = 3, Name = "CollegeAdmin" }
            );


            b.Entity<Country>().HasData(
                new Country { CountryId = 1, CountryName = "India", IsActive = true }  // ? NEW SEED
            );

            b.Entity<State>().HasData(
                new State { StateId = 1, StateName = "Uttar Pradesh", CountryId = 1, IsActive = true }
            );

            b.Entity<District>().HasData(
                new District { DistrictId = 1, StateId = 1, DistrictName = "Balrampur", IsActive = true }
            );

            b.Entity<OrganizationType>().HasData(
                new OrganizationType { OrganizationTypeId = 1, TypeName = "University", IsActive = true },
                new OrganizationType { OrganizationTypeId = 2, TypeName = "Affiliated College", IsActive = true },
                new OrganizationType { OrganizationTypeId = 3, TypeName = "Office", IsActive = true }
            );

            b.Entity<StatusMaster>().HasData(
                new StatusMaster { StatusId = 1, StatusName = "Published", IsActive = true },
                new StatusMaster { StatusId = 2, StatusName = "Draft", IsActive = true }
            );

            b.Entity<OuCategory>().HasData(
                new OuCategory { OuCategoryId = 1, OuCategoryName = "PLAN", IsActive = true },
                new OuCategory { OuCategoryId = 2, OuCategoryName = "NON-PLAN", IsActive = true }
            );

            b.Entity<LocationType>().HasData(
                new LocationType { LocationTypeId = 1, LocationTypeName = "Urban", IsActive = true },
                new LocationType { LocationTypeId = 2, LocationTypeName = "Rural", IsActive = true }
            );

            b.Entity<AccreditationAgency>().HasData(
                new AccreditationAgency { AgencyId = 1, AgencyCode = "NAAC", AgencyName = "National Assessment and Accreditation Council", IsActive = true },
                new AccreditationAgency { AgencyId = 2, AgencyCode = "AICTE", AgencyName = "All India Council for Technical Education", IsActive = true },
                new AccreditationAgency { AgencyId = 3, AgencyCode = "NCTE", AgencyName = "National Council for Teacher Education", IsActive = true },
                new AccreditationAgency { AgencyId = 4, AgencyCode = "PCI", AgencyName = "Pharmacy Council of India", IsActive = true },
                new AccreditationAgency { AgencyId = 5, AgencyCode = "BCI", AgencyName = "Bar Council of India", IsActive = true },
                new AccreditationAgency { AgencyId = 6, AgencyCode = "UGC", AgencyName = "University Grants Commission", IsActive = true }
            );
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var e in ChangeTracker.Entries<Organization>())
            {
                if (e.State == EntityState.Added) e.Entity.CreatedAt = DateTime.UtcNow;
                if (e.State == EntityState.Modified) e.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
