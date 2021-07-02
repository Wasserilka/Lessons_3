using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using System;
using Xunit;
using Moq;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mockLogger;
        private Mock<IHddMetricsRepository> mockRepository;

        public HddMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();

            controller = new HddMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<HddMetric>());

            var result = controller.GetByTimePeriod(DateTimeOffset.FromUnixTimeSeconds(1), DateTimeOffset.FromUnixTimeSeconds(100));

            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }
    }
}
