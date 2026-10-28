
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

        [HttpGet("by_groupname", Name ="GetStudentsByGroup")]

        public async Task<IActionResult> GetStudentsByGroupAsync([FromQuery] StudentGroupFilter filter, CancellationToken cancellationToken=default)
        {
         var students= await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);
            return Ok(students);
        }

       
        
        [HttpGet("by-lastname", Name = "GetStudentsByLastName")]
        public async Task<IActionResult> GetStudentsByLastNameAsync([FromQuery] StudentLastNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByLastNameAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpPost("create", Name = "CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentParams data, CancellationToken cancellationToken)
        {
            var student = new Student()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                GroupId = data.GroupId
            };
            var newStudent = await _studentService.AddStudentAsync(student, cancellationToken);

            return Ok(newStudent);
        }
        public record CreateStudentParams(string FirstName, string LastName, int GroupId);

    }

}
