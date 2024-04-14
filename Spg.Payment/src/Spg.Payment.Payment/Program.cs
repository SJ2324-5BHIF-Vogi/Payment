using Spg.Payment.Application.Validators;
using Spg.Payment.DomainModel.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Spg.Payment.Application.Services;
using Spg.Payment.DomainModel.Interfaces;
using Spg.Payment.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager Conf = builder.Configuration;

//Add db
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string useDb = Conf.GetSection("ConnectionStrings").GetValue<string>("UseDb");
Console.WriteLine(useDb);

// Create DB (JUST FOR TEST PURPOSE!!!!!)
DbContextOptions options = new DbContextOptionsBuilder()
.UseSqlite(connectionString)
.Options;
PaymentContext db = new PaymentContext(options);
//db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//builder.Services.AddDbContext<TennisBookingContext>(options => options.UseSqlite("Data Source=TennisBooking.db"));
builder.Services.AddDbContext<PaymentContext>(options =>
{
    if (!options.IsConfigured)
    {
        //if (useDb == "MySQL")
        //{
        //    options.UseMySQL(connectionString);
        //}
        //else
        if (useDb == "SQLite")
        {
            options.UseSqlite(connectionString);
        }
        else
        {
            throw new Exception("No Database selected");
        }
        //options.UseLazyLoadingProxies();
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Validators
builder.Services.AddScoped<IValidator<CreatePaymentDto>, CreatePaymentValidator>();
#endregion

#region Mediator
//builder.Services.AddMediatR((c) =>
//{
//    c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
//});
//builder.Services.AddMediatR(CreatePaymentDto);
#endregion

#region Services
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IWebhookService, WebhookService>();
builder.Services.AddScoped(typeof(Spg.Payment.Repository.GenericRepository));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
