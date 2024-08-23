using Archivium.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Archivium.DataAccess.Migartions;


[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.6")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Archivium.Domain.Entities.Categories.Category", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.ToTable("Categories");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Collections.Collection", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<long?>("AssetId")
                .HasColumnType("bigint");

            b.Property<long>("CategoryId")
                .HasColumnType("bigint");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<string>("Description")
                .HasColumnType("text");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("AssetId");

            b.HasIndex("CategoryId");

            b.HasIndex("UserId");

            b.ToTable("Collections");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Comments.Comment", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<string>("Content")
                .HasColumnType("text");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<long>("ItemId")
                .HasColumnType("bigint");

            b.Property<long?>("ParentId")
                .HasColumnType("bigint");

            b.Property<DateTime>("Time")
                .HasColumnType("timestamp without time zone");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("ItemId");

            b.HasIndex("ParentId");

            b.HasIndex("UserId");

            b.ToTable("Comments");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Commons.Asset", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<string>("Path")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.ToTable("Assets");

            b.HasData(
                new
                {
                    Id = 1L,
                    CreatedAt = new DateTime(2024, 6, 3, 7, 59, 23, 308, DateTimeKind.Utc).AddTicks(9035),
                    CreatedByUserId = 0L,
                    IsDeleted = false,
                    Name = "Picture",
                    Path = ""
                },
                new
                {
                    Id = 2L,
                    CreatedAt = new DateTime(2024, 6, 3, 7, 59, 23, 308, DateTimeKind.Utc).AddTicks(9040),
                    CreatedByUserId = 0L,
                    IsDeleted = false,
                    Name = "Picture2",
                    Path = ""
                });
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Fields.Field", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<long>("CollectionId")
                .HasColumnType("bigint");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<int>("FieldType")
                .HasColumnType("integer");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("CollectionId");

            b.ToTable("Fields");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Fields.FieldValue", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<long>("FieldId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<long>("ItemId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.Property<string>("Value")
                .HasColumnType("text");

            b.HasKey("Id");

            b.HasIndex("FieldId");

            b.HasIndex("ItemId");

            b.ToTable("FieldValues");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Items.Item", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<long>("CollectionId")
                .HasColumnType("bigint");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("CollectionId");

            b.ToTable("Items");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Items.ItemTag", b =>
        {
            b.Property<long>("ItemId")
                .HasColumnType("bigint");

            b.Property<long>("TagId")
                .HasColumnType("bigint");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<long>("Id")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("ItemId", "TagId");

            b.HasIndex("TagId");

            b.ToTable("ItemTags");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Likes.Like", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<long>("ItemId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.Property<long>("UserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.HasIndex("ItemId");

            b.HasIndex("UserId");

            b.ToTable("Likes");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Tags.Tag", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.HasKey("Id");

            b.ToTable("Tags");
        });

        modelBuilder.Entity("Majmuah.Domain.Entities.Users.User", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long>("CreatedByUserId")
                .HasColumnType("bigint");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("DeletedByUserId")
                .HasColumnType("bigint");

            b.Property<string>("Email")
                .HasColumnType("text");

            b.Property<string>("FirstName")
                .HasColumnType("text");

            b.Property<bool>("IsBlocked")
                .HasColumnType("boolean");

            b.Property<bool>("IsDeleted")
                .HasColumnType("boolean");

            b.Property<string>("LastName")
                .HasColumnType("text");

            b.Property<string>("PasswordHash")
                .HasColumnType("text");

            b.Property<string>("Phone")
                .HasColumnType("text");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp without time zone");

            b.Property<long?>("UpdatedByUserId")
                .HasColumnType("bigint");

            b.Property<int>("UserRole")
                .HasColumnType("integer");

            b.HasKey("Id");

            b.ToTable("Users");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Collections.Collection", b =>
        {
            b.HasOne("Majmuah.Domain.Entities.Commons.Asset", "Asset")
                .WithMany()
                .HasForeignKey("AssetId");

            b.HasOne("Archivium.Domain.Entities.Categories.Category", "Category")
                .WithMany("Collections")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Archivium.Domain.Entities.Users.User", "User")
                .WithMany("Collections")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Asset");

            b.Navigation("Category");

            b.Navigation("User");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Comments.Comment", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Items.Item", "Item")
                .WithMany("Comments")
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Archivium.Domain.Entities.Comments.Comment", "Parent")
                .WithMany("Replies")
                .HasForeignKey("ParentId");

            b.HasOne("Archivium.Domain.Entities.Users.User", "User")
                .WithMany("Comments")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Item");

            b.Navigation("Parent");

            b.Navigation("User");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Fields.Field", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Collections.Collection", "Collection")
                .WithMany("Fields")
                .HasForeignKey("CollectionId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Collection");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Fields.FieldValue", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Fields.Field", "Field")
                .WithMany()
                .HasForeignKey("FieldId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Archivium.Domain.Entities.Items.Item", "Item")
                .WithMany("FieldValues")
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Field");

            b.Navigation("Item");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Items.Item", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Collections.Collection", "Collection")
                .WithMany("Items")
                .HasForeignKey("CollectionId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Collection");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Items.ItemTag", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Items.Item", "Item")
                .WithMany("ItemTags")
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Archivium.Domain.Entities.Tags.Tag", "Tag")
                .WithMany("ItemTags")
                .HasForeignKey("TagId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Item");

            b.Navigation("Tag");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Likes.Like", b =>
        {
            b.HasOne("Archivium.Domain.Entities.Items.Item", "Item")
                .WithMany("Likes")
                .HasForeignKey("ItemId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("Archivium.Domain.Entities.Users.User", "User")
                .WithMany("Likes")
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Item");

            b.Navigation("User");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Categories.Category", b =>
        {
            b.Navigation("Collections");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Collections.Collection", b =>
        {
            b.Navigation("Fields");

            b.Navigation("Items");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Comments.Comment", b =>
        {
            b.Navigation("Replies");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Items.Item", b =>
        {
            b.Navigation("Comments");

            b.Navigation("FieldValues");

            b.Navigation("ItemTags");

            b.Navigation("Likes");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Tags.Tag", b =>
        {
            b.Navigation("ItemTags");
        });

        modelBuilder.Entity("Archivium.Domain.Entities.Users.User", b =>
        {
            b.Navigation("Collections");

            b.Navigation("Comments");

            b.Navigation("Likes");
        });
#pragma warning restore 612, 618
    }
}

