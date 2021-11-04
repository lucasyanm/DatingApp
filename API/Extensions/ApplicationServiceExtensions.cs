using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            //this 'this' makes this method an extension method
            //its not an argument to call the method
            this IServiceCollection a_Services,
            IConfiguration a_Config)
        {            
            // to add token jwt on application
            a_Services.AddScoped<ITokenService, TokenService>();

            //to add the connection to the database
            a_Services.AddDbContext<DataContext>(options => 
            {
               options.UseSqlite(a_Config.GetConnectionString("DefaultConnection"));
            });

            return a_Services;
        }
    }
}