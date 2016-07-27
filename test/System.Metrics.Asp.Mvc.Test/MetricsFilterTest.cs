using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Filters;
using Xunit;

namespace System.Metrics.Asp.Mvc.Test
{
    public class MetricsFilterTest 
    {
        [Fact]
        public async void TestCleanFilter()
        {
            // Arrange
            var subject = new MetricsFilter();
            var fellThrough = false;
            ResourceExecutionDelegate fakeExec = delegate() {
                fellThrough = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            // Act
            await subject.OnResourceExecutionAsync(null, fakeExec);

            // Assert
            fellThrough.Should().Be(true, "ResourceExecution was not routed though to the final handler delegate");
        }
    }
}