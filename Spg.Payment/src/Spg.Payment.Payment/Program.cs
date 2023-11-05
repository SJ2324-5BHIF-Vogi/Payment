using Spg.Payment.Application.Validators;
using Spg.Payment.DomainModel.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Validators
builder.Services.AddScoped<IValidator<CreatePaymentDto>, CreatePaymentValidator>();
#endregion

#region Mediator
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});
//builder.Services.AddMediatR(CreatePaymentDto);
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
