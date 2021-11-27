using Microsoft.Extensions.Options;

namespace Wishlist
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureOptions<InMemoryWishListOptions>()
                .AddTransient<IValidateOptions<InMemoryWishListOptions>, InMemoryWishListOptions>()
                .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<InMemoryWishListOptions>>().Value)
            ;
            services.AddSingleton<IWishList, InMemoryWishList>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
