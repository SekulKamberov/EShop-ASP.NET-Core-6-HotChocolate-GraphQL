using Microsoft.EntityFrameworkCore;

using EShop.API.Extensions;
using EShop.Data;
using EShop.Infrastructure.Mutations;
using EShop.Infrastructure.Queries;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args); 

    builder.Services.AddControllers();

//builder.Services.AddDbContextPool<EShopDbContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

builder.Services.AddDbContext<EShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityExtensions();
    builder.Services.AddCustomErrorFilters();

    var tokenParams = builder.Services.AddTokenValidationParameters(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins("http://localhost:3000")  
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
});

    builder.Services.AddServiceRegisterations(builder.Configuration);
    builder.Services.AddJWTAuthentication(tokenParams);
    builder.Services.AddAuthorization();
  
    builder.Services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddMutationType<Mutations>()
        .RegisterDbContext<EShopDbContext>()
        .AddType<StoreType>()
        .AddType<ProductType>()
        .AddTypeExtension<StoreQuery>()  
        .AddFiltering() 
        .AddSorting()
        .AddProjections()
        .AddAuthorization(); 

    var app = builder.Build();  

    //using (var scope = app.Services.CreateScope())
    //{
    //    var eShopDbContext = scope.ServiceProvider.GetRequiredService<EShopDbContext>();
    //    if (eShopDbContext != null)
    //        app.UseDataSeeding(eShopDbContext);
    //}

    app.UseRouting();

    app.UseCors(myAllowSpecificOrigins);

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGraphQL();

    app.Run();
