namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        private static int ORDER_NAME_MAX_LENGTH = 100;
        private static int ORDER_ADDRESS_MAX_LENGTH = 50;
        private static int ORDER_ADDRESS_LINE_MAX_LENGTH = 180;
        private static int ORDER_ADDRESS_ZIPCODE_MAX_LENGTH = 5;

        private static int ORDER_PAYMENT_CARD_NAME_MAX_LENGTH = 50;
        private static int ORDER_PAYMENT_CARD_NUMBER_MAX_LENGTH = 24;
        private static int ORDER_PAYMENT_EXPIRATION_MAX_LENGTH = 10;
        private static int ORDER_PAYMENT_CVV_MAX_LENGTH = 3;

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                productId => productId.Value,
                dbId => OrderId.Of(dbId)
                );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(ORDER_NAME_MAX_LENGTH)
                    .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(ORDER_ADDRESS_LINE_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                        .IsRequired();

                    addressBuilder.Property(a => a.ZipCode)
                       .HasMaxLength(ORDER_ADDRESS_ZIPCODE_MAX_LENGTH)
                       .IsRequired();
                });

            builder.ComplexProperty(
               o => o.BillingAddress, addressBuilder =>
               {
                   addressBuilder.Property(a => a.FirstName)
                       .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.LastName)
                       .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.EmailAddress)
                       .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.AddressLine)
                       .HasMaxLength(ORDER_ADDRESS_LINE_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.Country)
                       .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.State)
                       .HasMaxLength(ORDER_ADDRESS_MAX_LENGTH)
                       .IsRequired();

                   addressBuilder.Property(a => a.ZipCode)
                      .HasMaxLength(ORDER_ADDRESS_ZIPCODE_MAX_LENGTH)
                      .IsRequired();
               });

            builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(a => a.CardName)
                       .HasMaxLength(ORDER_PAYMENT_CARD_NAME_MAX_LENGTH)
                       .IsRequired();

                   paymentBuilder.Property(a => a.CardNumber)
                       .HasMaxLength(ORDER_PAYMENT_CARD_NUMBER_MAX_LENGTH)
                       .IsRequired();

                   paymentBuilder.Property(a => a.Expiration)
                       .HasMaxLength(ORDER_PAYMENT_EXPIRATION_MAX_LENGTH)
                       .IsRequired();

                   paymentBuilder.Property(a => a.CVV)
                       .HasMaxLength(ORDER_PAYMENT_CVV_MAX_LENGTH)
                       .IsRequired();

                   paymentBuilder.Property(a => a.PaymentMethod);
               });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}
