using AzureAD.Configs;
using AzureAD.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AzureAdOptions>(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddScoped<IAuth0Service, Auth0Service>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IExternalService<string>, ExternalService<string>>();
builder.Services.AddHttpClient<string>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x => x
	.AllowAnyMethod()
	.AllowAnyHeader()
	.SetIsOriginAllowed(origin => true) // allow any origin
	.AllowCredentials());

app.UseHttpsRedirection();

app.MapControllers(); // Map the controllers

app.Run();
