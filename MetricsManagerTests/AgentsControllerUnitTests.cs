﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = controller.RegisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetAgentsList_ReturnsOk()
        {
            var result = controller.GetAgentsList();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
