
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var key = Encoding.ASCII.GetBytes("AZ5wzWbarn97AjV7pqrnrxM3Ba7LrngWsGaa8trnaerzENy7");
    var issuer = "ComparAcademy";
    var audience = "ComparAcademy";

    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = audience,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        RequireExpirationTime =true,
        ValidateAudience =true,
        ValidateIssuer =true
    };
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuestionDal, QuestionDal>();
builder.Services.AddScoped<IQuestionServices, QuestionServices>();

builder.Services.AddScoped<IUserDal,UserDal>();
builder.Services.AddScoped<IUserServices, UserService>();

builder.Services.AddScoped<ICommentDal, CommentDal>();
builder.Services.AddScoped<ICommentServices, CommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
