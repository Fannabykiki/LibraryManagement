
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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBookCategoryDetail, BookCategoryDetailsRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBookRequestRepository, BookRequestRepository>();
builder.Services.AddScoped<IBorrowingDetailRepository, BorrowingDetailReposotory>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
    )
    );

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
