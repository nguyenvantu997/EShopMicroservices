namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        private static int CUSTOMER_NAME_MAX_LENGTH = 255;
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                customerId => customerId.Value,
                dbId => CustomerId.Of(dbId)
                );

            builder.Property(c => c.Name).HasMaxLength(CUSTOMER_NAME_MAX_LENGTH);
            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
