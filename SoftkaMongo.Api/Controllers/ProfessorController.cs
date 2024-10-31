using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.DataObjectTransfer;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly ILogger<ProfessorController> _logger;
    private readonly IAppmongoProfessorService AppmongoProfessorService;
    private readonly IMapper Mapper;

    public ProfessorController(ILogger<ProfessorController> logger, IMapper mapper, IAppmongoProfessorService appmongoProfessorService)
    {
        this._logger = logger;
        this.Mapper = mapper;
        this.AppmongoProfessorService = appmongoProfessorService;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        List<Professor> all = AppmongoProfessorService.GetAllProfessor();
        List<ProfessorDto> result = Mapper.Map<List<ProfessorDto>>(all);
        return Ok(result);
    }
    [HttpPost]
    public IActionResult InsertStudent(ProfessorDto professorDto)
    {
        Professor professor = Mapper.Map<Professor>(professorDto);
        AppmongoProfessorService.InsertProfessor(professor);
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent([FromRoute] string id)
    {

        AppmongoProfessorService.DeleteProfessor(id);
        return Ok();
    }
    [HttpGet("{id}")]
    public IActionResult GetStudent([FromRoute] string id)
    {
        Professor getProfessor = AppmongoProfessorService.GetProfessor(id);
        return Ok(Mapper.Map<ProfessorDto>(getProfessor));
    }
    [HttpPut]
    public IActionResult UpdateStudent(ProfessorDto professorDto)
    {
        Professor professor = Mapper.Map<Professor>(professorDto);
        Professor updateStudent = AppmongoProfessorService.UpdateProfessor(professor);
        return Ok(Mapper.Map<StudentDto>(updateStudent));
    }
}
