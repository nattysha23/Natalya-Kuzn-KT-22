using KuznNat_Kt_23.Filters.StudentFilters;
using KuznNat_Kt_23.ServiceExtensions;
using KuznNat_Kt_23.Database;
using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore;

public interface IStudentService
{
    public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    public Task<Student[]> GetStudentsByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken);


    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;

        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>()
                .Where(w => w.Group.Name == filter.GroupName && !w.IsDeleted)
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,

                    // Явно зануляем объект группы и отчество, чтобы они не выводились в JSON

                    Group = null
                })
        .ToArrayAsync(cancellationToken);

            return students;
        }
        public Task<Student[]> GetStudentsByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>()
                .Where(w => w.LastName == filter.LastName && !w.IsDeleted)
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    Group = null // Отсекаем зацикливание
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }

    }
}

