
using Microsoft.AspNetCore.Mvc;
using KuznNat_Kt_23.Filters.StudentFilters;

using KuznNat_Kt_23.Models;
using KuznNat_Kt_23.Filters.StudentFilters;
using KuznNat_Kt_23.Filters.GroupFilters;



namespace KuznNat_Kt_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController: ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

       [HttpGet("by-course", Name = "Get-GroupsByCourse")]
        public async Task<IActionResult> GetGroupsByCourse( [FromQuery] GroupCourseFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _groupService.GetGroupsByCourseAsync(filter, cancellationToken);
            return Ok(students);
        }



    }

}
