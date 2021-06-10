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

        public DbInicializador (UserManager<Persona> userManager, RoleManager<Rol> roleManager)
        {
            _userManager = userManager;
            _rolManager = roleManager;

        }

        public async void Seed()
        {
         
           
            //rolAlumno = await _rolManager.FindByNameAsync("Alumno");
            //rolProf = await _rolManager.FindByNameAsync("Profesor");
            //rolEmpleado = await _rolManager.FindByNameAsync("Empleado");

           
                IniciarRolAlumno();
         
                IniciarRolProf();
           
                IniciarRolEmpleado();
            

        }


        private void IniciarRolProf()
        {
            _rolManager.CreateAsync(new Rol() { Name = "Alumno" }).Wait();
            
        
        }
        private void IniciarRolAlumno()
        {
            _rolManager.CreateAsync(new Rol() { Name = "Empleado" }).Wait();


        }
        private void IniciarRolEmpleado()
        {
            _rolManager.CreateAsync(new Rol() { Name = "Profesor" }).Wait();


        }
      

    }
}
