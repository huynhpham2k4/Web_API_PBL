using Microsoft.EntityFrameworkCore;
using Web_API_PBL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin", builder =>
	{
		builder.WithOrigins("http://localhost:8080") // Địa chỉ ứng dụng Vue.js
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

// Cấu hình DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
});

// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Sử dụng CORS trước HTTPS Redirection
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
