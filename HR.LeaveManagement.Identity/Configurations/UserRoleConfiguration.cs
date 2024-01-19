using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "0b58a531-c821-434b-b927-920c29a36786",
                    UserId = "46053c3e-2801-4867-bdbc-b97ed1d5a327"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0b58a531-c821-434b-b927-920c29aaafb7",
                    UserId = "9fe9a794-d82d-48a8-9999-f6a90be439ac"
                }
            );
        }
    }
}
