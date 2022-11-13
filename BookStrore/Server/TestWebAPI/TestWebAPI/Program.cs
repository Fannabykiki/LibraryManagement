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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBookCategoryDetail, BookCategoryDetailsRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBookRequestRepository, BookRequestRepository>();
builder.Services.AddScoped<IBorrowingDetailRepository, BorrowingDetailReposotory>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
//builder.Services.AddCors(options =>
//    options.AddPolicy("CorsPolicy",
//        builder => builder
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowAnyOrigin()
//            .WithOrigins("http://localhost:3000/").AllowAnyMethod().AllowAnyHeader()
//    )
//    );

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => 
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters{
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

app.Run();
