# System.Metrics.Asp.Mvc

This is an extension library to **System.Metrics** it's seamless integration into ASP.Net MVCs architecture.

It can be installed and configured in your ASP.Net Startup code as follows:

```csharp

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add MVC.
        services.AddMvc();

        // Add the metrics service(s)
        services.AddMetrics()
            .UseStatsD()
            .UseUdp() // Or .UseUdp("hostname", port, [package MTU size])
    }
    
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        if(env.IsDevelopment())
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
        }

        if(env.IsProduction() || env.IsStaging())
        {
            // Enable Metrics
        }

        app.UseMvc();
    }
}

```