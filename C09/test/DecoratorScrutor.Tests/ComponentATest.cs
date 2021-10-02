using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DecoratorScrutor
{
    public class ComponentATest
    {
        public class Operation : ComponentATest
        {
            [Fact]
            public void Should_return_Hello_from_ComponentA()
            {
                // Arrange
                var sut = new ComponentA();

                // Act
                var result = sut.Operation();

                // Assert
                Assert.Equal("Hello from ComponentA", result);
            }
        }
    }
}
