
using Microsoft.AspNetCore.Mvc;
using KuznNat_Kt_23.Filters.StudentFilters;

using KuznNat_Kt_23.Models;
using KuznNat_Kt_23.Filters.StudentFilters;
   


namespace KuznNat_Kt_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController: ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost("by_groupname", Name ="GetStudentsByGroup")]

        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken=default)
        {
         var students= await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);
            return Ok(students);
        }
        [HttpPost("by-lastname", Name = "GetStudentsByLastName")]
        public async Task<IActionResult> GetStudentsByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByLastNameAsync(filter, cancellationToken);
            return Ok(students);
        }

    }

}
