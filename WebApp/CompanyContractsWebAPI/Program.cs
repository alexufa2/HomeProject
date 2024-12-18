using CompanyContractsWebAPI.DbRepositories;
using CompanyContractsWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionStr = builder.Configuration.GetConnectionString("DefaultConnectionString");
// Add services to the container.
builder.Services.AddSingleton<IRepository<CompanyPurpose>, CompanyPurposeRepository>(
    c => new CompanyPurposeRepository(connectionStr));

builder.Services.AddSingleton<IRepository<Company>, CompanyRepository>(
    c => new CompanyRepository(connectionStr));

builder.Services.AddSingleton<IRepository<Good>, GoodRepository>(
    c => new GoodRepository(connectionStr));

builder.Services.AddSingleton<IRepository<CompanyGoods>, CompanyGoodsRepository>(
    c => new CompanyGoodsRepository(connectionStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
