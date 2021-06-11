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

        public DbInicializador(UserManager<Persona> userManager, RoleManager<Rol> roleManager)
        {
            _userManager = userManager;
            _rolManager = roleManager;

        }

        public async void Seed()
        {
            string rolAlu = "Alumno";
            string rolEmp = "Empleado";
            string rolProfesor = "Profesor";

            //rolAlumno = await _rolManager.FindByNameAsync("Alumno");
            //rolProf = await _rolManager.FindByNameAsync("Profesor");
            //rolEmpleado = await _rolManager.FindByNameAsync("Empleado");

            if (!_rolManager.Roles.Any()) //Si no hay roles
            {
                //no hay roles
                //creo los roles

                IniciarRol(rolAlu);
                IniciarRol(rolEmp);
                IniciarRol(rolProfesor);
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


        }
        private async void IniciarRol(string nombre)
        {
            _rolManager.CreateAsync(new Rol() { Name = nombre }).Wait();
        }
    }
}
