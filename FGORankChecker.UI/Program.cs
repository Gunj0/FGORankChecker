using FGORankChecker.UI.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/servantList", () =>
{
    var servantEntities = new ViewModel().GetServantEntities();
    var json = servantEntities
        .Select(servantEntity => new
        {
            servantEntity.Id,
            servantEntity.Name,
            servantEntity.Rank,
            servantEntity.Rare,
            servantEntity.ClassType,
            servantEntity.NPType,
            servantEntity.Range,
            servantEntity.Thumbnail
        })
        .ToArray();
    return json;
})
.WithName("GetServantList");

app.Run();
