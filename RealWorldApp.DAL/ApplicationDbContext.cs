using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealWorldApp.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL;
public class ApplicationDbContext : ApiAuthorizationDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
    {

    }
    public DbSet<Articles> Title { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Tags> Tag { get; set; }
    public DbSet<Favorities> FollowerUsername { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}