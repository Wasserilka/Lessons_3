using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsManager.Controllers;
using MetricsManager.DAL;
using System;
using Xunit;
using Moq;
using AutoMapper;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;
        private Mock<ILogger<AgentsController>> mockLogger;
        private Mock<IAgentsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public AgentsControllerUnitTests()
        {
            mockRepository = new Mock<IAgentsRepository>();
            mockLogger = new Mock<ILogger<AgentsController>>();
            mockMapper = new Mock<IMapper>();

            controller = new AgentsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_CreateAgent_From_Repository()
        {
            var result = controller.CreateAgent(
                50000,
                @"http:\\localhost:50000");

            mockRepository.Verify(repository => repository.Create(
                It.IsAny<AgentInfo>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_EnableAgentById_From_Repository()
        {
            var result = controller.CreateAgent(
                50000,
                @"http:\\localhost:50000");

            mockRepository.Verify(repository => repository.Enable(
                It.IsAny<long>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_DisableAgentById_From_Repository()
        {
            var result = controller.CreateAgent(
                50000,
                @"http:\\localhost:50000");

            mockRepository.Verify(repository => repository.Disable(
                It.IsAny<long>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_GetAgentsList_From_Repository()
        {
            mockRepository.Setup(repository => repository.GetEnabledAgents())
                .Returns(new List<AgentInfo>());

            var result = controller.GetAgentsList();

            mockRepository.Verify(repository => repository.GetEnabledAgents());
        }
    }
}
