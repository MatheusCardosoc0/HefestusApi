using Api.Utilities;
using HefestusApi.Services.Interfaces;
using HefestusApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Repositories;
using HefestusApi.Repositories.Pessoal.Interfaces;
using HefestusApi.Repositories.Pessoal;
using HefestusApi.Services.Pessoal.Interfaces;
using HefestusApi.Services.Pessoal;
using HefestusApi.Services.Materiais.Interfaces;
using HefestusApi.Services.Materiais;
using HefestusApi.Repositories.Materiais;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Repositories.Financeiro.Interfaces;
using HefestusApi.Services.Financeiro.Interfaces;
using HefestusApi.Services.Financeiro;
using HefestusApi.Repositories.Financeiro;
using HefestusApi.Repositories.Vendas;
using HefestusApi.Repositories.Vendas.Interfaces;
using HefestusApi.Services.Vendas.Interfaces;
using HefestusApi.Services.Vendas;
using HefestusApi.Models.Data;
using HefestusApi.Repositories.Administracao.Interfaces;
using HefestusApi.Repositories.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using HefestusApi.Services.Administracao;

namespace HefestusApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "https://localhost:7263/",
                    ValidAudience = "https://localhost:7263/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
            });

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            builder.Services.AddScoped<ISystemLocationRepository, SystemLocationRepository>();
            builder.Services.AddScoped<ISystemLocationService, SystemLocationService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IPaymentOptionsRepository, PaymentOptionsRepository>();
            builder.Services.AddScoped<IPaymentOptionService, PaymentOptionService>();

            builder.Services.AddScoped<IPaymentConditionRepository, PaymentConditionRepository>();
            builder.Services.AddScoped<IPaymentConditionService, PaymentConditionService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            builder.Services.AddScoped<IProductGroupService, ProductGroupService>();

            builder.Services.AddScoped<IProductSubGroupRepository, ProductSubGroupRepository>();
            builder.Services.AddScoped<IProductSubGroupService, ProductSubGroupService>();

            builder.Services.AddScoped<IProductFamilyRepository, ProductFamilyRepository>();
            builder.Services.AddScoped<IProductFamilyService, ProductFamilyService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ICityService, CityService>();

            builder.Services.AddScoped<IPersonGroupRepository, PersonGroupRepository>();
            builder.Services.AddScoped<IPersonGroupService, PersonGroupService>();

            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IPersonService, PersonService>();

            builder.Services.AddScoped<TokenService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}