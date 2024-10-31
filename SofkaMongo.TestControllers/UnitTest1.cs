using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SoftkaMongo.Api.Controllers;
using SoftkaMongo.BusinessLogicLayer.Services.AppMongo;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.DataObjectTransfer;

namespace SofkaMongo.TestControllers
{
   
    public class UnitTest1
    {
        private readonly Mock<IAppmongoProfessorService> _mockRepo;
        private readonly ProfessorController _controller;
        private readonly Mock<ILogger<ProfessorController>> _logger;
        private readonly Mock<IMapper> Mapper;
        public UnitTest1()
        {
            _mockRepo = new Mock<IAppmongoProfessorService>();
            _logger = new Mock<ILogger<ProfessorController>>();
            Mapper = new Mock<IMapper>();
            _controller = new ProfessorController(_logger.Object,Mapper.Object,_mockRepo.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.GetStudent("63d0a3ec871e6949cdf37121");
            Assert.IsType<ProfessorDto>(result);
        }
    }
}
