using KSO_SalesHub.Models.Simulation;
using KSO_SalesHub.Services;
using KSO_SalesHub.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddCors(option =>
{
	option.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowedToAllowWildcardSubdomains()
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader()
		.Build());
});

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<SimulationDbContext>(
	options => options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection1"),
		builder => builder.EnableRetryOnFailure()
		));

builder.Services.Configure<CookiePolicyOptions>(opt =>
{
	opt.CheckConsentNeeded = context => true;
	opt.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddHsts(opt =>
{
	opt.IncludeSubDomains = true;
	opt.MaxAge = TimeSpan.FromDays(1);
});

builder.Services.AddAntiforgery(opt =>
{
	opt.SuppressXFrameOptionsHeader = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		.AddCookie(options =>
		{
			options.LoginPath = "/Home/Login";
		});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromHours(24);
	options.IOTimeout = TimeSpan.FromHours(24);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<CalculatorUtils>();
builder.Services.AddScoped<HttpClientAPIHandler>();

builder.Services.AddScoped<EmailServices>();
builder.Services.AddScoped<SimulationServices>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseRotativa();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization(); 
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=PublicWeb}/{action=Index}/{id?}");

app.Run();
