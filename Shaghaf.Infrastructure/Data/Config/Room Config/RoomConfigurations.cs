using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shaghaf.Core.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure.Data.Config.Room_Config
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(room => room.Type)
                    .HasConversion(
                    (RType) => RType.ToString(),
                    (RType) => (RoomType)Enum.Parse(typeof(RoomType), RType, true)

                    );

            builder.Property(room => room.Plan)
                    .HasConversion(
                    (RPlan) => RPlan.ToString(),
                    (RPlan) => (RoomPlan)Enum.Parse(typeof(RoomPlan), RPlan, true)

                    );

            builder.Property(room => room.Rate)
                .HasColumnType("decimal(12,1)");

       

            builder.Property(room => room.Price)
                .HasColumnType("decimal(12,2)");
        }
    }
}
