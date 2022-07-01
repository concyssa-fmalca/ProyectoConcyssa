using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO=true;
});

builder.Services.AddBundling()
    //.UseDefaults(_env)
    .UseNUglify();
var app = builder.Build();

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
app.UseSession();



//app.UseBundling(bundles =>
//{
//    bundles.AddJs("/bundles/jquerytemp.js")
//        .Include("~/js/plugins/jquery-1.11.2.min.js");

    //    bundles.AddJs("/bundles/scripts-temp.js")
    //              .Include("~/assets/js/jquery/jquery_ui/jquery-ui.min.js")
    //              .Include("~/assets/js/plugins/highcharts/highcharts.js")
    //              .Include("~/assets/js/plugins/c3charts/d3.min.js")
    //              .Include("~/assets/js/plugins/c3charts/c3.min.js")
    //              .Include("~/assets/js/plugins/circles/circles.js")
    //              .Include("~/assets/js/plugins/magnific/jquery.magnific-popup.js")
    //              .Include("~/assets/js/utility/utility.js")
    //              .Include("~/assets/js/demo/demo.js")
    //              .Include("~/assets/js/main.js")
    //              .Include("~/assets/js/pages/dashboard1.js")
    //              .Include("~/assets/sweetalert2.js")
    //              .Include("~/assets/bootstrap-treeview.js")
    //              .Include("~/js/jquery/jquery_ui/jquery_ui.min.js")
    //              .Include("~/assets/js/plugins/select2/select2.min.js");

    //    bundles.AddCss("/bundles/css-temp.css")
    //              .Include("~/assets/fonts/icomoon/icomoon.css")
    //              .Include("~/assets/js/plugins/fullcalendar/fullcalendar.min.css")
    //              .Include("~/assets/js/plugins/select2/css/core.css")
    //              .Include("~/assets/js/plugins/magnific/magnific-popup.css")
    //              .Include("~/assets/js/plugins/c3charts/c3.min.css")
    //              .Include("~/assets/skin/default_skin/css/theme.css")
    //              .Include("~/assets/allcp/forms/css/forms.css")
    //              .Include("~/assets/bootstrap-treeview.css")
    //              .Include("~/assets/sweetalert2.css")
    //              .Include("~/css/jquery.autocomplete.css");
//});

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });


app.Run();
