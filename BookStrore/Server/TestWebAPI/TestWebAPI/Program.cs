using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Data.Repositories.Interfaces;
using BookStore.Services.CategoryService;
using BookStore.API.Services.BookService;
using BookStore.API.Services.CategoryService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.API.Services.UserService;
using BookStore.Data.Repositories.Implements;
using BookStore.Service.Services.Loggerservice;
using BookStore.API.Extensions;
using BookStore.API;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using BookStore.Data.Entities;
using Common.Enums;
using BookStore.Service.Services.ShippingService;

internal class Program
{
    private static void Main(string[] args)
    {
        static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Books>("Books");
            builder.EntitySet<BookBorrowingRequest>("BookBorrowingRequests");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<User>("Users");
            builder.EntitySet<Shipping>("Shippings");
            return builder.GetEdmModel();
        }
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
		builder.Services.AddScoped<ICategoryService, CategoryService>();
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IBookCategoryDetail, BookCategoryDetailsRepository>();
        builder.Services.AddScoped<IShippingDetailRepository, ShippingDetailRepository>();
        builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
        builder.Services.AddScoped<IShippingService, ShippingService>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IBookRequestRepository, BookRequestRepository>();
        builder.Services.AddScoped<IBorrowingDetailRepository, BorrowingDetailReposotory>();
        builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
        builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Expand().Count().OrderBy().SetMaxTop(100));
        builder.Services.AddSignalR();

        builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
        {
            build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        }));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JwtConstant.Issuer,
                    ValidAudience = JwtConstant.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key)),
                };
            }
        );

        var configuration = builder.Configuration;
        builder.Services.AddDbContext<BookStoreContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DBConnString"));
        });
        builder.Services.AddAuthorization(op =>
        {
            op.AddPolicy(UserRoles.Admin, p => p.RequireClaim(UserRoles.ClaimUser,"Admin"));
        }
        );

        builder.Services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var logger = app.Services.GetRequiredService<ILoggerManager>();
        app.ConfigureExceptionHandler(logger);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("corspolicy");

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHub<BookHub>("/bookhub");

        app.Run();
    }
}