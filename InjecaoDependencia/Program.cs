using InjecaoDependencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedScoped<ISendService,EmailSendService>("EmailSend");
builder.Services.AddKeyedScoped<ISendService,SmsSendService>("SmsSend");
builder.Services.AddKeyedScoped<ISendService,PushSendService>("PushSend");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
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

app.MapControllers();

app.Run();


