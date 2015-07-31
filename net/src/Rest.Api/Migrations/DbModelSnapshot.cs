using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Rest.Api.Models;

namespace RestApiMigrations
{
    [ContextType(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("Rest.Api.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Rest.Api.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .Required();

                    b.Key("Id");
                });

            builder.Entity("Rest.Api.Models.Product", b =>
                {
                    b.Reference("Rest.Api.Models.Category")
                        .InverseCollection()
                        .ForeignKey("CategoryId");
                });
        }
    }
}
