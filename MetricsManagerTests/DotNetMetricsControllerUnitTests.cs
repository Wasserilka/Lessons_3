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
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private Mock<IDotNetMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public DotNetMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            mockMapper = new Mock<IMapper>();

            controller = new DotNetMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_GetMetricsFromAgent_From_Repository()
        {
            mockRepository.Setup(repository => repository.GetFromAgentByTimePeriod(
                It.IsAny<long>(),
                It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>()))
                .Returns(new List<DotNetMetric>());

            var result = controller.GetMetricsFromAgent(
                50000,
                DateTimeOffset.FromUnixTimeSeconds(1),
                DateTimeOffset.FromUnixTimeSeconds(100));

            mockRepository.Verify(repository => repository.GetFromAllClusterByTimePeriod(
                It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_GetMetricsFromAllCluster_From_Repository()
        {
            mockRepository.Setup(repository => repository.GetFromAllClusterByTimePeriod(
                It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>()))
                .Returns(new List<DotNetMetricAgentId>());

            var result = controller.GetMetricsFromAllCluster(
                DateTimeOffset.FromUnixTimeSeconds(1),
                DateTimeOffset.FromUnixTimeSeconds(100));

            mockRepository.Verify(repository => repository.GetFromAllClusterByTimePeriod(
                It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>()),
                Times.AtMostOnce());
        }
    }
}
