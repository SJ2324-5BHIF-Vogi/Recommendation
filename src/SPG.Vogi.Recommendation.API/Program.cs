using RabbitMQ.Client;
using SPG.Vogi.Recommendation.Application;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;
using SPG.Vogi.Recommendation.Repository;

var builder = WebApplication.CreateBuilder(args);


string connectionString = "mongodb://localhost:27000";

builder.Services.AddTransient<IMongoDbSettings>(sp => new MongoDbSettings
{
    ConnectionString = connectionString,
    DatabaseName = "RecommendationDb"
});
builder.Services.AddTransient<IRecommService, RecommService>();
builder.Services.AddTransient<IMongoRepository<Posts>, MongoRepository<Posts>>();
builder.Services.AddTransient<IMongoRepository<User>, MongoRepository<User>>();

//RabbitMQS
builder.Services.AddSingleton<IConnectionFactory>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var rabbitMqConfig = configuration.GetSection("RabbitMq").Get<RabbitMqConfig>();

    var factory = new ConnectionFactory
    {
        HostName = rabbitMqConfig.Host,
        UserName = rabbitMqConfig.Username,
        Password = rabbitMqConfig.Password
    };

    return factory;
});

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.AddDebug();
});

builder.Services.AddSingleton<IModel>(provider =>
{
    var connectionFactory = provider.GetRequiredService<IConnectionFactory>();
    var connection = connectionFactory.CreateConnection();
    return connection.CreateModel();
});


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
