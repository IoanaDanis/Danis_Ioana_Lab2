using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Danis_Ioana_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Danis_Ioana_Lab2Context' not found.");

builder.Services.AddDbContext<Danis_Ioana_Lab2Context>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
