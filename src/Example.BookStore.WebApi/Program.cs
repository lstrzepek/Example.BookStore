using Example.BookStore.Catalog;
using Example.BookStore.Catalog.Contracts;
using Example.BookStore.WebApi;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
.AddJsonFile("appsettings.json")
.AddEnvironmentVariables("BookStore_");
  // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
  // .AddJsonFile($"appsettings.{Microsoft.AspNetCore.Hosting.EnvironmentName}.json", optional: true);
// Add services to the container.
Console.WriteLine($"Using connection: {builder.Configuration.GetConnectionString("Catalog")}");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Book).Assembly);
builder.Services.AddDbContext<CatalogContext>(opt => opt
    .LogTo(Console.WriteLine)
    .UseNpgsql(builder.Configuration.GetConnectionString("Catalog")));
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationRequestPreProcessor<>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeline<,>));
builder.Services.AddValidatorsFromAssemblyContaining<RegisterBook>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();


app.Run();
