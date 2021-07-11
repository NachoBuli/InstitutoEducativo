using InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Data
{
    public class DbInicializador : IDbInicializador
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _rolManager;
        private DbContextInstituto _context;

        public DbInicializador(UserManager<Persona> userManager, RoleManager<Rol> roleManager, DbContextInstituto context)
        {
            _userManager = userManager;
            _rolManager = roleManager;
            _context = context;
        }

        public void Seed()
        {
            
            

            //rolAlumno = await _rolManager.FindByNameAsync("Alumno");
            //rolProf = await _rolManager.FindByNameAsync("Profesor");
            //rolEmpleado = await _rolManager.FindByNameAsync("Empleado");

            if (!_rolManager.Roles.Any()) //Si no hay roles
            {
                //no hay roles
                //creo los roles

                IniciarRol(Helpers.rolAlu);
                IniciarRol(Helpers.rolEmpl);
                IniciarRol(Helpers.rolProf);

               
            }
            else
            {

                //if (_rolManager.RoleExistsAsync(rolAlu).Wait()) //si no existe el rol alumno
                //{
                //    //Si no existe, lo creo
                //    IniciarRol(rolAlu);
                //}
                ////y demas



            }
            CreoDatos();


        }
        private  void IniciarRol(string nombre)
        {
            _rolManager.CreateAsync(new Rol() { Name = nombre }).Wait();
        }

        private  void CreoDatos()
        {

            if (!_context.Personas.Any())
            {

                 crearProfesor();
                 crearAlumno();
                 crearEmpleado();
                 
               
            }
        }

      

        private  void crearProfesor()
        {
            Persona profesor = new Profesor
            {
                Id = Guid.NewGuid(),
                Nombre = "profesor",
                UserName = "profesor@profesor.com",
                Email = "profesor@profesor.com",
                Direccion = "Avenida Libertador, Buenos Aires",
                Telefono = "1122345678",
                Dni = "12345621",
                Apellido = "profe",
                FechaAlta = DateTime.Now,
                Legajo = "Profesor-12346",
                CalificacionesRealizadas = new List<Calificacion>(),
                MateriasCursadasActivas = new List<MateriaCursada>()
            };
            var resultadoDeProf = _userManager.CreateAsync(profesor, Helpers.password).Result;

            if (resultadoDeProf.Succeeded)
            {
                 var result = _userManager.AddToRoleAsync(profesor, Helpers.rolProf).Result;
            }

        }

        private void crearAlumno()
        {
            Carrera carrera = new Carrera
            {
                CarreraId = Guid.NewGuid(),
                Nombre = "Analisis De Sistemas"

            };

            _context.Carreras.Add(carrera);
            _context.SaveChanges();

            crearMateria("Programacion", carrera, "p-01","codigo");
            crearMateria("Ingles", carrera,"i-03", "English");
            crearMateria("Programacion2",carrera,"p-02", "codigo");
            crearMateria("Base De Datos", carrera, "b-01", "datos");
            crearMateria("Base De Datos 2", carrera, "b-02", "datos");

            Persona alumno = new Alumno
            {
                Id = Guid.NewGuid(),
                Nombre = "alumno",
                UserName = "alumno@alumno.com",
                Email = "alumno@alumno.com",
                CarreraId = carrera.CarreraId,
                Carrera = carrera,
                Direccion = "Laprida, Buenos Aires",
                Telefono = "1122345678",
                Dni = "12335621",
                Apellido = "Alu",
                FechaAlta = DateTime.Now,
                Activo = true,
                NumeroMatricula = 12345,


            };
            var resultadoAlumno = _userManager.CreateAsync(alumno, Helpers.password).Result;

            if (resultadoAlumno.Succeeded)
            {
             var result =  _userManager.AddToRoleAsync(alumno, Helpers.rolAlu).Result;
            }
        }

        private void crearEmpleado()
        {
            Persona empleado = new Empleado
            {
                Id = Guid.NewGuid(),
                Nombre = "empleado",
                UserName = "empleado@empleado.com",
                Email = "empleado@empleado.com",
                Direccion = "Aguero, Buenos Aires",
                Telefono = "1122845678",
                Dni = "17335621",
                Apellido = "Empl",
                FechaAlta = DateTime.Now,
                Legajo = "Empleado-12345"
            };

            var resultadoEmpleado = _userManager.CreateAsync(empleado, Helpers.password).Result;

            if (resultadoEmpleado.Succeeded)
            {
                var result = _userManager.AddToRoleAsync(empleado, Helpers.rolEmpl).Result;
            }
        }

        private void crearMateria(string nombre, Carrera carrera, string codigo, string desc)
        {

            Materia materia = new Materia
            {
                MateriaId = Guid.NewGuid(),
                Nombre = nombre,
                CodigoMateria = codigo,
                Descripcion = desc,
                CupoMaximo = 2,
                MateriasCursadas = new List<MateriaCursada>(),
                Calificaciones = new List<Calificacion>(),
                CarreraId = carrera.CarreraId,
                Carrera = carrera
            };

            _context.Materias.Add(materia);
            _context.SaveChanges();

            crearMateriaCursada(materia);
        }
        private void crearMateriaCursada(Materia materia)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Nombre == "profesor");
            MateriaCursada materiaCursada = new MateriaCursada
            {
                MateriaCursadaId = Guid.NewGuid(),
                Nombre = materia.Nombre + (materia.MateriasCursadas.Count + 1),
                Anio = 1,
                Cuatrimestre = 2,
                Activo = true,
                MateriaId = materia.MateriaId,
                Materia = materia,
                ProfesorId = profesor.Id,
                Profesor = profesor,
                AlumnoMateriaCursadas = new List<AlumnoMateriaCursada>(),
                Calificaciones = new List<Calificacion>()
            };

            _context.MateriaCursadas.Add(materiaCursada);
            _context.SaveChanges();
        }
    }
}
