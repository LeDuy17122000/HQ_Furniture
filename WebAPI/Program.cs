using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Authorization;
using WebAPI.Services;
var builder = WebApplication.CreateBuilder(args);

#region Add Services

builder.Services.AddControllers();

// SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

#region JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",
        policy => policy.RequireRole("Admin"));

    options.AddPolicy("EmployeeOnly",
        policy => policy.RequireRole("Employee"));

    options.AddPolicy("CustomerOnly",
        policy => policy.RequireRole("Customer"));
});
builder.Services.AddSingleton<IAuthorizationPolicyProvider,
    PermissionPolicyProvider>();

builder.Services.AddScoped<IAuthorizationHandler,
    PermissionHandler>();
#endregion

#region Repository

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

#endregion

#region Service

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

// JWT (sẽ tạo ở bước tiếp theo)
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IJwtService, JwtService>();
// builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(

    typeof(ProductProfile),

    typeof(CategoryProfile),

    typeof(UserProfile),

    typeof(RoleProfile),

    typeof(PermissionProfile),

    typeof(ReviewProfile),

    typeof(ProductImageProfile),

    typeof(PostProfile),

    typeof(OrderProfile),

    typeof(OrderDetailProfile)

);
builder.Services.AddAutoMapper(typeof(UserProfile));
#endregion
#region AutoMapper

builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

#endregion
#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HQ Furniture API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,

            Scheme = "Bearer",

            BearerFormat = "JWT",

            In = Microsoft.OpenApi.Models.ParameterLocation.Header,

            Description = "Nhập JWT theo dạng: Bearer {token}"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,

                            Id="Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();