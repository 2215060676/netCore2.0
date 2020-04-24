using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using There.Model;
using There.Services;

namespace There
{
    public class Startup
    {

        //ConfigureServices 用于注册服务
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入
            //单例注入模式
            services.AddSingleton(typeof(IwelcomeService), typeof(welcomeService));
           // services.AddSingleton<IwelcomeService, welcomeService>();

            //添加MVC服务
            services.AddMvc();

            //添加一个容器
            services.AddScoped<IRepsoitory<Student>, InMeMoryRepository>();

            ////域模式汪入，在一个请求域内，只产生一个实例对象
            //services.AddScoped(typeof(IwelcomeService), typeof(welcomeService));
            //services.AddScoped<IwelcomeService, welcomeService>();
  
            ////通过接口形式注入服务
            //// Transient瞬态模式注入，每一次请求都会产生一个新的实例
            //services.AddTransient(typeof(IwelcomeService), typeof(welcomeService));
            //services.AddTransient<IwelcomeService, welcomeService>();



        }
        // IConfiguration configUration net core内有依赖注入，配置文件可以写在appsettings。json里面，然后拿出来
        //ILogger<Startup> logger 是一个记录日志的东西
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env,
            IConfiguration configUration
            ,IwelcomeService welcomeService
            ,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                ////开发者报错，异常页面
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ////上线了用这个
                app.UseExceptionHandler();
            }


            //启用映射 找到相应的文件
            app.UseDefaultFiles();

            //使用mvc默认路由
            //app.UseMvcWithDefaultRoute();

           

            //不使用默认路由 配置路由
            app.UseMvc(buider=>
            {
                buider.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
            #region  通过路径返回数据
            //app.Use(next =>
            //{
            //    logger.LogInformation("app use()...");
            //    return async httpContext =>
            //    {
            //        logger.LogInformation("...async httpcontext");
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            logger.LogInformation("first");
            //            await httpContext.Response.WriteAsync("first");
            //        }
            //        else
            //        {
            //            logger.LogInformation("next(httpContext)");
            //            await next(httpContext);
            //        }
            //    };          
            //   });

            ////中间件
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path="/welcome"
            //});
            #endregion



            app.Run(async (context) =>
            {
              //  var welcome = configUration["Welcome"];
                var welcome1 = welcomeService.GetMessAge();
                await context.Response.WriteAsync(welcome1);
            });
        }
    }
}
