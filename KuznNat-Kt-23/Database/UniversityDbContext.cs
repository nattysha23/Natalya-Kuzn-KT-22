using KuznNat_Kt_23.Database.Configurations;
using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore;

namespace KuznNat_Kt_23.Database
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }


        //Добавляем таблицы
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Student> Specialitys { get; set; }
        DbSet<Student> Grades { get; set; }
        DbSet<Student> Disciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialityConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());

        }
    }
}