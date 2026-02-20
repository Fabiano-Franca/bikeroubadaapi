
using BikeRoubada.Api.AuxiliaryModels;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BikeRoubada.Api.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

    }
}
