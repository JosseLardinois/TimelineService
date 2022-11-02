using TimelineService.Processor;
using TimelineService.Model;
using Amazon.Runtime;
using TimelineService.Interfaces;
using TimelineService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
builder.Services.AddDefaultAWSOptions(awsOptions);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddHostedService<TimelineFollowersProcessor>();//background process
builder.Services.AddHostedService<TimelinePostsProcessor>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFollowersRepository, FollowersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
