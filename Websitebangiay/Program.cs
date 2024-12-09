using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Websitebangiay.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebbangiayContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings: DefaultConnection"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();


app.MapControllerRoute(
name:
"areas",
pattern: " { area: exists}/{ controller=Home}/{ action=Index}/{ id ?}"
);


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
