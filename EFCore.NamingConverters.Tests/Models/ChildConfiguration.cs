using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.NamingConverters.Tests.Models
{
    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable("MY_ChildTable", "MY_SchemaName");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name);
            builder.HasOne<Parent>().WithMany().HasForeignKey(x => x.ParentId);
        }
    }
}
