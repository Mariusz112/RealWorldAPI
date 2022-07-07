﻿using RealWorldAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RealWorldApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Articles> Articles { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Users> Users { get; set; }
}
