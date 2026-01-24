using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMqCustomClient;

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
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractDoneRepository, ContractDoneRepository>();


builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddSingleton<RabbitMQSettings>();

builder.Services.AddSingleton(x =>
{
    var settings = x.GetRequiredService<IOptions<RabbitMQSettings>>();
    var rabbitMQSettings = settings.Value;
    return new RabbitMqSender<ContractCreated>(rabbitMQSettings.Host,
                                  rabbitMQSettings.VirtualHost,
                                  rabbitMQSettings.Port,
                                  rabbitMQSettings.ContarctCreateSender.User.Name,
                                  rabbitMQSettings.ContarctCreateSender.User.Pass);
});

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