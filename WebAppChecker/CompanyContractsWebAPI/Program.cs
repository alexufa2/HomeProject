using CompanyContractsWebAPI.BusinessLogic;
using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string policyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials();
    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

string? connectionStr = builder.Configuration.GetConnectionString("DefaultConnectionString");

//Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionStr));
builder.Services.AddTransient<IRepository<Contract>, ContractRepository>();
builder.Services.AddTransient<IContractDoneRepository, ContractDoneRepository>();


builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddSingleton<RabbitMQSettings>();
builder.Services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddSingleton<IMessageProcessor, MessageProcessor>();

builder.Services.AddHostedService<RabbitMqListener>();

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
app.MapHub<ContractsHub>("/contractsHub");
app.Run();