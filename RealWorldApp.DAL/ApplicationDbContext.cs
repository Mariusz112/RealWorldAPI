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
    public DbSet<Articles> Article { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Tags> Tag { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Articles>()
        .Property(p => p.Slug)
        .HasColumnType("nvarchar(5000)")
        .HasComputedColumnSql("[Title] + '-' + cast([Id] as nvarchar(2000))");
        base.OnModelCreating(builder);


        builder.Entity<Articles>()
            .HasOne(p => p.Author);

        builder.Entity<Articles>()
            .HasMany(p => p.Favorited);

        builder.Entity<Articles>()
            .HasMany(p => p.Comments);

        builder.Entity<Articles>()
            .HasMany(p => p.Tags)
            .WithMany(x => x.Articles);

        builder.Entity<User>()
            .HasMany(p => p.LikedArticle);

        builder.Entity<User>()
            .HasMany(p => p.FollowedUsers);
    }

}