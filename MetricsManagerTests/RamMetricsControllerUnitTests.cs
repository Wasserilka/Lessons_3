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
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private Mock<IRamMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public RamMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            mockMapper = new Mock<IMapper>();

            controller = new RamMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_GetMetricsFromAgent_From_Repository()
        {
            mockRepository.Setup(repository => repository.GetFromAgentByTimePeriod(
                It.IsAny<long>(),
                It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetric>());

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
                .Returns(new List<RamMetricAgentId>());

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
