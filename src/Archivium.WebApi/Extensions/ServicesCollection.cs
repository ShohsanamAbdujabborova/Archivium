using Archivium.DataAccess.UnitOfWorks;
using Archivium.Service.Helpers;
using Archivium.Service.Services.Assets;
using Archivium.Service.Services.Categories;
using Archivium.Service.Services.Collections;
using Archivium.Service.Services.Comments;
using Archivium.Service.Services.Fields;
using Archivium.Service.Services.FieldValues;
using Archivium.Service.Services.Items;
using Archivium.Service.Services.ItemTags;
using Archivium.Service.Services.Likes;
using Archivium.Service.Services.Tags;
using Archivium.Service.Services.Users;
using Archivium.WebApi.ApiServices;
using Archivium.WebApi.ApiServices.Accounts;
using Archivium.WebApi.ApiServices.Categories;
using Archivium.WebApi.ApiServices.Collections;
using Archivium.WebApi.ApiServices.Comments;
using Archivium.WebApi.ApiServices.Fields;
using Archivium.WebApi.ApiServices.FieldValues;
using Archivium.WebApi.ApiServices.Items;
using Archivium.WebApi.ApiServices.ItemTags;
using Archivium.WebApi.ApiServices.Likes;
using Archivium.WebApi.ApiServices.Tags;
using Archivium.WebApi.ApiServices.Users;
using Archivium.WebApi.Middlewares;
using Archivium.WebApi.Validators.Accounts;
using Archivium.WebApi.Validators.Assets;
using Archivium.WebApi.Validators.Categories;
using Archivium.WebApi.Validators.Collections;
using Archivium.WebApi.Validators.Comments;
using Archivium.WebApi.Validators.Fields;
using Archivium.WebApi.Validators.FieldValues;
using Archivium.WebApi.Validators.Items;
using Archivium.WebApi.Validators.ItemTags;
using Archivium.WebApi.Validators.Likes;
using Archivium.WebApi.Validators.Tags;
using Archivium.WebApi.Validators.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Archivium.WebApi.Extensions;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IFieldService, FieldService>();
        services.AddScoped<IFieldValueService, FieldValueService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IItemTagService, ItemTagService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<ITagService, TagService>();
    }

    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserApiService, UserApiService>();
        services.AddScoped<IAccountApiService, AccountApiService>();
        services.AddScoped<ITagApiService, TagApiService>();
        services.AddScoped<ILikeApiService, LikeApiService>();
        services.AddScoped<IItemTagApiService, ItemTagApiService>();
        services.AddScoped<IItemApiService, ItemApiService>();
        services.AddScoped<IFieldValueApiService, FieldValueApiService>();
        services.AddScoped<IFieldApiService, FieldApiService>();
        services.AddScoped<ICommentApiService, CommentApiService>();
        services.AddScoped<ICollectionApiService, CollectionApiService>();
        services.AddScoped<ICategoryApiService, CategoryApiService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<UserCreateModelValidator>();
        services.AddTransient<UserUpdateModelValidator>();
        services.AddTransient<UserChangePasswordModelValidator>();

        services.AddTransient<LoginModelValidator>();
        services.AddTransient<ResetPasswordModelValidator>();
        services.AddTransient<SendCodeModelValidator>();
        services.AddTransient<ConfirmCodeModelValidator>();

        services.AddTransient<AssetCreateModelValidator>();

        services.AddTransient<CategoryCreateModelValidator>();
        services.AddTransient<CategoryUpdateModelValidator>();

        services.AddTransient<CollectionCreateModelValidator>();
        services.AddTransient<CollectionUpdateModelValidator>();

        services.AddTransient<CommentCreateModelValidator>();
        services.AddTransient<CommentUpdateModelValidator>();

        services.AddTransient<FieldCreateModelValidator>();
        services.AddTransient<FieldUpdateModelValidator>();

        services.AddTransient<FieldValueCreateModelValidator>();
        services.AddTransient<FieldValueUpdateModelValidator>();

        services.AddTransient<ItemCreateModelValidator>();
        services.AddTransient<ItemUpdateModelValidator>();

        services.AddTransient<ItemTagCreateModelValidator>();
        services.AddTransient<ItemTagUpdateModelValidator>();

        services.AddTransient<LikeCreateModelValidator>();

        services.AddTransient<TagCreateModelValidator>();
        services.AddTransient<TagUpdateModelValidator>();
    }

    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
    }

    public static void AddInjectEnvironmentItems(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
        EnvironmentHelper.JWTKey = app.Configuration.GetSection("JWT:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHours = app.Configuration.GetSection("JWT:LifeTime").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
        EnvironmentHelper.PageSize = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageSize").Value);
        EnvironmentHelper.PageIndex = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageIndex").Value);
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}