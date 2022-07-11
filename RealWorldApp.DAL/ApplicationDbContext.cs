using Microsoft.EntityFrameworkCore;
using RealWorldApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Articles> Title { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Tags> Tag { get; set; }
    public DbSet<Users> Username { get; set; }
    public DbSet<Favorities> FollowerUsername { get; set; }
}