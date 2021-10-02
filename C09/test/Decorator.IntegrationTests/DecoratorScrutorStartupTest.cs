using Microsoft.AspNetCore.Mvc.Testing;

namespace Decorator.IntegrationTests
{
    public class DecoratorScrutorStartupTest : StartupTest<DecoratorScrutor.Startup>
    {
        public DecoratorScrutorStartupTest(WebApplicationFactory<DecoratorScrutor.Startup> webApplicationFactory)
            : base(webApplicationFactory)
        {
        }
    }
}
