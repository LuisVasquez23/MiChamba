﻿// <auto-generated />
using System;
using MiChamba.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiChamba.Migrations
{
    [DbContext(typeof(MiChambaDbContext))]
    partial class MiChambaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiChamba.Models.Calificacion", b =>
                {
                    b.Property<int>("IdCalificacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_CALIFICACION");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCalificacion"));

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COMENTARIO");

                    b.Property<DateTime>("FechaCalificacion")
                        .HasColumnType("datetime")
                        .HasColumnName("FECHA_CALIFICACION");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int")
                        .HasColumnName("ID_EMPRESA");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.Property<int>("calificacion")
                        .HasColumnType("int")
                        .HasColumnName("CALIFICACION");

                    b.HasKey("IdCalificacion");

                    b.HasIndex("IdEmpresa");

                    b.HasIndex("IdUsuario");

                    b.ToTable("CALIFICACION");
                });

            modelBuilder.Entity("MiChamba.Models.Empresa", b =>
                {
                    b.Property<int>("IdEmpresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_EMPRESA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpresa"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DESCRIPCION");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DIRECCION");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IMAGEN");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("IdEmpresa");

                    b.ToTable("EMPRESA");
                });

            modelBuilder.Entity("MiChamba.Models.Oferta", b =>
                {
                    b.Property<int>("IdOferta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_OFERTA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOferta"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DESCRIPCION");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("datetime")
                        .HasColumnName("FECHA_PUBLICACION");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int")
                        .HasColumnName("ID_EMPRESA");

                    b.Property<string>("Requisitos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("REQUISITOS");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TITULO");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UBICACION");

                    b.HasKey("IdOferta");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("OFERTA");
                });

            modelBuilder.Entity("MiChamba.Models.Postulacion", b =>
                {
                    b.Property<int>("IdPostulacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_POSTULACION");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPostulacion"));

                    b.Property<string>("EstadoPostulacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ESTADO_POSTULACION");

                    b.Property<DateTime>("FechaPostulacion")
                        .HasColumnType("datetime")
                        .HasColumnName("FECHA_POSTULACION");

                    b.Property<int>("IdOferta")
                        .HasColumnType("int")
                        .HasColumnName("ID_OFERTA");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("IdPostulacion");

                    b.HasIndex("IdOferta");

                    b.HasIndex("IdUsuario");

                    b.ToTable("POSTULACION");
                });

            modelBuilder.Entity("MiChamba.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("APELLIDO");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IMAGEN");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("IdUsuario");

                    b.ToTable("USUARIO");
                });

            modelBuilder.Entity("MiChamba.Models.Calificacion", b =>
                {
                    b.HasOne("MiChamba.Models.Empresa", "Empresa")
                        .WithMany("Calificaciones")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiChamba.Models.Usuario", "Usuario")
                        .WithMany("Calificaciones")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MiChamba.Models.Oferta", b =>
                {
                    b.HasOne("MiChamba.Models.Empresa", "Empresa")
                        .WithMany("Ofertas")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("MiChamba.Models.Postulacion", b =>
                {
                    b.HasOne("MiChamba.Models.Oferta", "Oferta")
                        .WithMany("Postulaciones")
                        .HasForeignKey("IdOferta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiChamba.Models.Usuario", "Usuario")
                        .WithMany("Postulaciones")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oferta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MiChamba.Models.Empresa", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("Ofertas");
                });

            modelBuilder.Entity("MiChamba.Models.Oferta", b =>
                {
                    b.Navigation("Postulaciones");
                });

            modelBuilder.Entity("MiChamba.Models.Usuario", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("Postulaciones");
                });
#pragma warning restore 612, 618
        }
    }
}