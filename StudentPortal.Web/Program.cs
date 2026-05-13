using Microsoft.EntityFrameworkCore;
using StudentPortal.Web;
using StudentPortal.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("StudentPortal")));

//builder.Services.AddSingleton<ApplicationDbContext, ApplicationDbContext>();
//builder.Services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
//builder.Services.AddTransient<ApplicationDbContext, ApplicationDbContext>();

var app = builder.Build();

var host = app.Migrate<ApplicationDbContext>();

// migrate the database.  Best practice = in Main, using service scope
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        // for demo purposes, delete the database & migrate on startup so 
        // we can start with a clean slate
        //context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
