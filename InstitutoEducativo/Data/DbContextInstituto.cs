﻿using InstitutoEducativo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Data
{
    public class DbContextInstituto : DbContext
    {
        public DbContextInstituto(DbContextOptions options) : base(options) // constructor contexto
        {

        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<AlumnoMateriaCursada> AlumnoMateriaCursadas { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaCursada> MateriaCursadas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            #region N:M Alumno MateriaCursada -> AlumnoMateriaCursada

            modelbuilder.Entity<AlumnoMateriaCursada>()
                   .HasKey(am => new { am.AlumnoId, am.MateriaCursadaId });

            modelbuilder.Entity<AlumnoMateriaCursada>()
                .HasOne(ma => ma.Alumno)
                .WithMany(a => a.AlumnosMateriasCursadas)
                .HasForeignKey(ma => ma.AlumnoId);

            modelbuilder.Entity<AlumnoMateriaCursada>()
                .HasOne(ma => ma.MateriaCursada)
                .WithMany(m => m.AlumnoMateriaCursadas)
                .HasForeignKey(ma => ma.MateriaCursadaId);

            #endregion


        }
    }

}
