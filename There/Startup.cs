using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using There.Data;
using There.Model;
using There.Services;

namespace There
{
    public class Startup
    {
        //构造函数注入
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
         //   services.AddScoped<IRepsoitory<Student>, InMeMoryRepository>();//这个是静态的，没有连数据库的时候用到的,用来参考
            services.AddScoped<IRepsoitory<Student>, EfCoreRepository>();

      

            //连接数据的写法
            //  var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            var connectionString = _configuration.GetConnectionString("DefaultConnection"); //另一种写法
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


                  //注入IdentityDbContext
            services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("There")));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();


            services.Configure<IdentityOptions>(options =>
            {

                //password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 1;

                //lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //user settings 
                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM-._@+";
                options.User.RequireUniqueEmail = false;
                
            });

            //string server = _configuration["database:Server"];
            //string Name = _configuration["database:Name"];
            //string Uid = _configuration["database:Uid"];
            //string Pwd = _configuration["database:Pwd"];
            ////Add-Migration 为迁移数据命令，ResultFirst提供迁移名称。执行后会在项目根目录下生成migrations文件夹。文件夹下会生成相关的类（通过这些类代码生成数据库）
            ////Add-Migration ResultFirst
            ////Update-Database 执行该命令，我生成数据库
            ////var connectio = @"server=.;Database=ResultDBmeo;UId=sa;Pwd=sa";
            //var connectio = $"server={server};Database={Name};UId={Uid};Pwd={Pwd}";
            ////把上下文注入进来
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseSqlServer(connectio);
            //});
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
            //  app.UseMvcWithDefaultRoute();


            //启用映射 找到相应的文件 导入node_modules里面的文件
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath="/node_modules",
                FileProvider=new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            });


            //启用身份验证
            app.UseAuthentication();

            //不使用默认路由 配置路由
            app.UseMvc(buider =>
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


            #region  在页面上输出数据，可以看一下
            //app.Run(async (context) =>
            //{
            //    //  var welcome = configUration["Welcome"];
            //    var welcome1 = welcomeService.GetMessAge();
            //    await context.Response.WriteAsync(welcome1);
            //});
            #endregion

        }
    }
}
