using Mango.Services.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();



void ApplyMigration()
{
    // Create a new scoped lifetime for resolving services (like AppDbContext)
    using (var scope = app.Services.CreateScope())
    {
        // Get an instance of AppDbContext from the scoped service provider
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Check if there are any pending migrations (migrations that haven't been applied to the DB yet)
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            // Apply all pending migrations to the database
            _db.Database.Migrate();
        }
    }
}
