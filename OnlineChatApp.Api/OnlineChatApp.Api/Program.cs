using OnlineChatApp.Api.Controllers.ChatHub;
using OnlineChatApp.Api.Functions.Message;
using OnlineChatApp.Api.Functions.UserFrined;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionString"]);
}); 

builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<ChatHub>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();


app.UseEndpoints(endpoint =>
{
	endpoint.MapControllers();
	endpoint.MapHub<ChatHub>("/ChatHub");
});

app.Run();
