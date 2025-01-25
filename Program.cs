using System.Security.Claims;

Dictionary<string, List<string>> gamesMap = new()
{
  {"player1", new List<string>(){"Mortal Combact", "Mario"}},
  {"player2", new List<string>(){"Mission Impossible", "Gangster Crime City", "Zuma"}}
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/players", () => gamesMap)
  .RequireAuthorization(policy =>
  {
      policy.RequireRole("admin");
  });

app.MapGet("/mygames", (ClaimsPrincipal user) =>
{
    ArgumentNullException.ThrowIfNull(user.Identity?.Name);
    var username = user.Identity.Name;

    if (!gamesMap.ContainsKey(username))
    {
        return Results.Empty;
    }

    return Results.Ok(gamesMap[username]);
})
.RequireAuthorization(policy =>
  {
      policy.RequireRole("player");
  });

app.Run();
