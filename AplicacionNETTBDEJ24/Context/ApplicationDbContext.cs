using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AplicacionNETTBDEJ24.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionNETTBDEJ24.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { 

        }


    public DbSet<Alumno> Alumno { get; set; }

    public DbSet<Profesor> Profesor { get; set; }

    public DbSet<Materias> Materias { get; set; }

    public DbSet<Kardex> Kardex { get; set; }
    }
}
