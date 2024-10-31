using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.DataObjectTransfer;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectMatterController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IAppmongoSubjectMatterService AppmongoSubjectMatterService;
    private readonly IMapper Mapper;

    public SubjectMatterController(ILogger<StudentController> logger, IMapper mapper, IAppmongoSubjectMatterService appmongoSubjectMatterService)
    {
        this._logger = logger;
        this.Mapper = mapper;
        this.AppmongoSubjectMatterService = appmongoSubjectMatterService;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        List<SubjectMatter> all = AppmongoSubjectMatterService.GetAllSubjectMatter();
        List<SubjectMatterDto> result = Mapper.Map<List<SubjectMatterDto>>(all);
        return Ok(result);
    }
    [HttpPost]
    public IActionResult InsertStudent(SubjectMatterDto subjectMatterDto)
    {
        SubjectMatter subjectMatter = Mapper.Map<SubjectMatter>(subjectMatterDto);
        AppmongoSubjectMatterService.InsertSubjectMatter(subjectMatter);
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent([FromRoute] string id)
    {
        AppmongoSubjectMatterService.DeleteSubjectMatter(id);
        return Ok();
    }
    [HttpGet("{id}")]
    public IActionResult GetStudent([FromRoute] string id)
    {
        SubjectMatter getSubjectMatter = AppmongoSubjectMatterService.GetSubjectMatter(id);
        return Ok(Mapper.Map<StudentDto>(getSubjectMatter));
    }
    [HttpPut]
    public IActionResult UpdateStudent(SubjectMatterDto subjectMatterDto)
    {
        SubjectMatter subjectMatter = Mapper.Map<SubjectMatter>(subjectMatterDto);
        SubjectMatter updateSubjectMatter = AppmongoSubjectMatterService.UpdateSubjectMatter(subjectMatter);
        return Ok(Mapper.Map<StudentDto>(updateSubjectMatter));
    }
}
