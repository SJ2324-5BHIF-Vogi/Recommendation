using SPG.Vogi.Recommendation.Application;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;
using SPG.Vogi.Recommendation.Repository;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IMongoDbSettings>(sp => new MongoDbSettings
{
    ConnectionString = connectionString,
    DatabaseName = "mongodb"
});
builder.Services.AddTransient<IRecommService, RecommService>();
builder.Services.AddTransient<IMongoRepository<Posts>, MongoRepository<Posts>>();
builder.Services.AddTransient<IMongoRepository<User>, MongoRepository<User>>();



// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
