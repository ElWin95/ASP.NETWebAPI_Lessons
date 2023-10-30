using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopAppP416.Data;
using ShopAppP416.Dtos.ProductDtos;
using ShopAppP416.Mapper;

namespace ShopAppP416
{
    public static class RegisterServices
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers().AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });
        }
    }
}
