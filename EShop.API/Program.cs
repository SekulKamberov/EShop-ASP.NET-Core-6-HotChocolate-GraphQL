using Microsoft.EntityFrameworkCore;

using EShop.API.Extensions;
using EShop.Data;
using EShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args); 

    builder.Services.AddControllers(); 

    builder.Services.AddDbContextPool<EShopDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

    builder.Services.AddIdentityExtensions();
    builder.Services.AddCustomErrorFilters();

    var tokenParams = builder.Services.AddTokenValidationParameters(builder.Configuration);

    builder.Services.AddCors();

    builder.Services.AddServiceRegisterations(builder.Configuration);
    builder.Services.AddJWTAuthentication(tokenParams);
    builder.Services.AddAuthorization();
  
    builder.Services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddSorting()
        .AddFiltering()
        .AddAuthorization(); 

    var app = builder.Build();  

    //using (var scope = app.Services.CreateScope())
    //{
    //    var eShopDbContext = scope.ServiceProvider.GetRequiredService<EShopDbContext>();
    //    if (eShopDbContext != null)
    //        app.UseDataSeeding(eShopDbContext);
    //}

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGraphQL();

    app.Run();
