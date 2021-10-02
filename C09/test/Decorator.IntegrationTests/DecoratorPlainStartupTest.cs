using Microsoft.AspNetCore.Mvc.Testing;

namespace Decorator.IntegrationTests
{
    public class DecoratorPlainStartupTest : StartupTest<DecoratorPlain.Startup>
    {
        public DecoratorPlainStartupTest(WebApplicationFactory<DecoratorPlain.Startup> webApplicationFactory)
            : base(webApplicationFactory)
        {
        }
    }

}
