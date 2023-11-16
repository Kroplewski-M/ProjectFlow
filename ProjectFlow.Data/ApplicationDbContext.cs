using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectFlow.Models.Business_Models;
using Task = ProjectFlow.Models.Business_Models.Task;
using TaskStatus = ProjectFlow.Models.Business_Models.TaskStatus;

namespace ProjectFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Workspace>Workspaces { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //WORKSPACE
            builder.Entity<Workspace>().HasOne(b =>b.User).WithMany().HasForeignKey(b=>b.UserId);

            //TASKS
            builder.Entity<Task>().HasOne(t=>t.TaskStatus).WithMany().HasForeignKey(t=>t.TaskStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Task>().HasOne(t=>t.Workspace).WithMany().HasForeignKey(t=>t.WorkspaceId).OnDelete(DeleteBehavior.Restrict);

            //RENAMING IDENTITY TABLES
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            //INSERTING DEFAULT TASKSTATUSES
            builder.Entity<TaskStatus>().HasData(
                new TaskStatus { Id = 1,Name = "To Do"},
                new TaskStatus { Id = 2, Name = "Doing" },
                new TaskStatus { Id = 3, Name = "Finished" }
            );
        }
    }
}
