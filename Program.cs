Dictionary<string, List<string>> gamesMap = new()
{
  {"player1", new List<string>(){"Mortal Combact", "Mario"}},
  {"player2", new List<string>(){"Mission Impossible", "Gangster Crime City", "Zuma"}}
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => gamesMap)
  .RequireAuthorization();

app.Run();
