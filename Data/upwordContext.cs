using Microsoft.EntityFrameworkCore;
using upword.Api.Entities;

namespace upword.Api.Data;

public class upwordContext(DbContextOptions<upwordContext> options)
    : DbContext(options)
{
    public DbSet<Word> Words => Set<Word>();
}