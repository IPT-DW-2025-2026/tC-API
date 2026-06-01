using API.Data;
using API.Data.Seed;
using API.Tools;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



// add Swagger tool to document the API
// Add the Swagger package
// <PackageReference Include="Swashbuckle.AspNetCore" Version="8.1.4" />  // for .NET Framework 8
// <PackageReference Include="Swashbuckle.AspNetCore" Version="10.1.5" />   // for .NET Framework 10
builder.Services.AddSwaggerGen(c => {
   c.SwaggerDoc("v1", new OpenApiInfo {
      Title = "Minha API de gestão de Fotos",
      Version = "v1",
      Description = "API para gestão de categorias, fotografias e utilizadores"
   });
   /*
     // Caminho para o XML gerado
       var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
       c.IncludeXmlComments(xmlPath);
   */
});


// *******************************************************************
// Instalar o package
// Microsoft.AspNetCore.Authentication.JwtBearer
//
// using Microsoft.IdentityModel.Tokens;
// *******************************************************************
// JWT Settings
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// I can use both cookie and JWT for authentication
builder.Services.AddAuthentication(options => { })
   .AddCookie("Cookies", options => {
      options.LoginPath = "/Identity/Account/Login";
      options.AccessDeniedPath = "/Identity/Account/AccessDenied";
   })
   .AddJwtBearer("Bearer", options => {
      options.TokenValidationParameters = new TokenValidationParameters {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtSettings["Issuer"],
         ValidAudience = jwtSettings["Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(key)
      };
   });


// configuração do JWT
builder.Services.AddScoped<TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
   app.UseMigrationsEndPoint();

   // use the seed methods
   app.UseItToSeedSqlServer();

   // start the 'middleware' Swagger
   app.UseSwagger();
   app.UseSwaggerUI();

}
else {
   app.UseExceptionHandler("/Home/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
