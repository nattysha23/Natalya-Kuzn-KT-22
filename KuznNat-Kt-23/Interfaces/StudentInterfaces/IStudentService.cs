using KuznNat_Kt_23.Filters.StudentFilters;
using KuznNat_Kt_23.ServiceExtensions;
using KuznNat_Kt_23.Database;
using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore;

public interface IStudentService
{
    //public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    public Task<string> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    public Task<Student[]> GetStudentsByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken);
    public Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);

    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;

        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
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
        }*/

        //если хотим только список фамилий
        public async Task<string> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            // Выбираем только фамилии из базы данных.
            var lastNames = await _dbContext.Set<Student>()
                .Where(w => w.Group.Name == filter.GroupName && !w.IsDeleted)
                .Select(s => s.LastName) // Оставляем только LastName
                .ToArrayAsync(cancellationToken);

            // Соединяем массив строк в одну строку через запятую.
            return string.Join(", ", lastNames);
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

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            // Проверка на наличие группы по внешнему ключу
            var groupExists = await _dbContext.Groups.AnyAsync(g => g.GroupId == student.GroupId, cancellationToken);


            if (!groupExists)
                throw new Exception($"Группа {student.GroupId} не найдена");

            // Добавляем нового студента с флагом IsDeleted = false
            student.IsDeleted = false;
            await _dbContext.Students.AddAsync(student, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            return student;  // Возвращаем только что созданный объект
        }

    }
}

