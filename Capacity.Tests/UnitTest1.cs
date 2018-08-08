using System;
using bigmojo.net.capacity.api.model;
using react.Controllers;
using Xunit;

namespace Capacity.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Project project = new Project();
            project.firstName = "hi";
            Assert.Equal("hi", project.firstName);
            
        }
    }
}
