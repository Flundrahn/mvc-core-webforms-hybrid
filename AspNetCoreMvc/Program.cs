using Data.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();
app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var showSql = builder.Configuration.GetValue<bool>("NHibernate:ShowSql", defaultValue: false);
NHibernateHelper.InitSessionFactory(connectionString, showSql);

app.Run();

// Make Program class accessible for integration testing
public partial class Program { }
