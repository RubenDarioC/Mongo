using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.DataObjectTransfer;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IAppmongoStudentService AppmongoStudentService;
    private readonly IMapper Mapper;
    private IAppmongoStudentService @object;

    public StudentController(ILogger<StudentController> logger, IMapper mapper, IAppmongoStudentService appmongoStudentService)
    {
        this._logger = logger;
        this.Mapper = mapper;
        this.AppmongoStudentService = appmongoStudentService;
    }

    public StudentController(IMapper mapper, IAppmongoStudentService @object)
    {
        Mapper = mapper;
        this.@object = @object;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        List<Students> all = AppmongoStudentService.GetAllStudents();
        List<StudentDto> result = Mapper.Map<List<StudentDto>>(all);
        return Ok(result);
    }
    [HttpPost]
    public IActionResult InsertStudent(StudentDto studentDto)
    {
        Students student = Mapper.Map<Students>(studentDto);
        AppmongoStudentService.InsertStudent(student);
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent([FromRoute] string id)
    {
        AppmongoStudentService.DeleteStudent(id);
        return Ok();
    }
    [HttpGet("{id}")]
    public IActionResult GetStudent([FromRoute] string id)
    {
        Students getStudent = AppmongoStudentService.GetStudent(id);
        return Ok(Mapper.Map<StudentDto>(getStudent));
    }
    [HttpPut]
    public IActionResult UpdateStudent(StudentDto studenDto)
    {
        Students students = Mapper.Map<Students>(studenDto);
        Students updateStudent = AppmongoStudentService.UpdateStudent(students);
        return Ok(Mapper.Map<StudentDto>(updateStudent));
    }
}
