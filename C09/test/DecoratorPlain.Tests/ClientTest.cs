using Moq;
using System;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text;
using ForEvolve.Testing.AspNetCore.Http;

namespace DecoratorPlain
{
    public class ClientTest
    {
        public class ExecuteAsync : ClientTest
        {
            private readonly HttpContextHelper httpContextHelper = new HttpContextHelper();

            [Fact]
            public async Task Should_response_write_the_result_of_the_component_operation()
            {
                // Arrange
                var componentMock = new Mock<IComponent>();
                componentMock.Setup(x => x.Operation()).Returns("Test Value").Verifiable();
                var sut = new Client(componentMock.Object);
                
                // Act
                await sut.ExecuteAsync(httpContextHelper.HttpContextMock.Object);

                // Assert
                httpContextHelper.HttpResponseFake.BodyShouldEqual("Operation: Test Value");
            }
        }
    }
}