using KuznNat_Kt_23.Filters.StudentFilters;
using KuznNat_Kt_23.ServiceExtensions;
using KuznNat_Kt_23.Database;
using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore;
using KuznNat_Kt_23.Filters.GroupFilters;

public interface IGroupService
{

    public Task<string> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken);



    public class GroupService : IGroupService
    {
        private readonly UniversityDbContext _dbContext;

        public GroupService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetGroupsByCourseAsync(GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            // Выбираем только названия групп из базы данных.
            var groupNames = await _dbContext.Groups
                .Where(g => g.Course == filter.Course && !g.IsDeleted)
                .Select(s => s.Name) // Оставляем только Name
                .ToArrayAsync(cancellationToken);

            // Соединяем массив строк в одну строку через запятую.
            return string.Join(", ", groupNames);
        }



    }
}

