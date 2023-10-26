namespace Medication.Data;

using Medication.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Medication> Medication => Set<Medication>();
}