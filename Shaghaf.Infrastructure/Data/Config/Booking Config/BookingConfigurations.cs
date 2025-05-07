using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities.BookingEntities;

public class BookingConfigurations : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(booking => booking.Status)
            .HasConversion(
                (BStatus) => BStatus.ToString(),
                (BStatus) => (BookingStatus)Enum.Parse(typeof(BookingStatus), BStatus, true)
            );

        builder.Property(booking => booking.Discount)
            .HasColumnType("decimal(12,2)");

        // تحديث الكود لاستخدام TotalAmount بدلاً من Amount
        builder.Property(booking => booking.TotalAmount)
            .HasColumnType("decimal(12,2)");
    }
}
