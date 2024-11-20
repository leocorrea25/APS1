﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Domain.Entities.Guest>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Guest> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.DocumentId)
                .Property(e => e.IdNumber);

            builder.OwnsOne(e => e.DocumentId)
                .Property(e => e.DocumentType);//lambda

        }
    }
}
