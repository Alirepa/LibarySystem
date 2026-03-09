using LibrarySystem.Data;
using LibrarySystem.Data.Data;
using LibrarySystem.Web.Components;
using LibrarySystem.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Databas
builder.Services.AddDbContextFactory<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// Våra services
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<LoanService>();

var app = builder.Build();

// Skapa databas vid startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();