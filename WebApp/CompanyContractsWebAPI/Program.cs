using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string policyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionStr = builder.Configuration.GetConnectionString("DefaultConnectionString");

//Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionStr));
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepository<Good>, GoodRepository>();
builder.Services.AddScoped<ICompanyGoodPriceRepository, CompanyGoodPriceRepository>();

var app = builder.Build();

using (var context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>())
{
    DbInintializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);
app.UseAuthorization();
app.MapControllers();
app.Run();