# Instituto Educativo 📖

## Objetivos 📋
Desarrollar un sistema, que permita la administración general de un Instituto Educativo (de cara a los Empleados): Profesores, Alumnos, Materias, Cursos, Calificaciones, Carreras, etc., como así también, permitir a los Profesores, realizar calificaciones y de cara a los Alumnos matricularse en las materias pendientes.
Utilizar Visual Studio 2019 preferentemente y crear una aplicación utilizando ASP.NET MVC Core (versión a definir por el docente 2.2 o 3.1).

<hr />

## Enunciado 📢
La idea principal de este trabajo práctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, el cual relevó e identificó la información aquí contenida. 
A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación, 

Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará a conseguir la información ya un poco más procesada. 
Es importante destacar, que este proceso, no debe esperar a ser en clase; es importante, que junten algunas consultas, sea de índole funcional o técnicas, en lugar de cada consulta enviarla de forma independiente.

Las consultas que sean realizadas por correo deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>

2.< xxxxxxxx>


# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relación del paciente con Turno es 1:1 o 1:N?

2.Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?

<hr />

### Proceso de ejecución en alto nivel ☑️
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Crear las relaciones entre las entidades
 - Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext. 
 - Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades en el DbContext.
 - Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Realizar un sistema de login con al menos los roles equivalentes a <Usuario Cliente> y <Usuario Administrador> (o con permisos elevados).
 - Si el proyecto lo requiere, generar el proceso de registración. 
 - Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente> sólo podrá tomar acción en el sistema, en base al rol que tiene.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos del lado de los permisos.
 - Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
 
<hr />

## Entidades 📄

- Usuario
- Empleado
- Profesor
- Alumno
- Carrera
- Materia
- MateriaCursada
- Calificacion

`Importante: Todas las entidades deben tener su identificador unico. Id o <ClassNameId>`

`
Las propiedades descriptas a continuación, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones.
`

**Usuario**
```
- Nombre
- Email
- FechaAlta
- Password
```

**Alumno**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Activo
- Email
- NumeroMatricula
- MateriasCursadas
- Carrera
- Calificaciones
```

**Profesor**
```
- Nombre
- Apellido
- Telefono
- Direccion
- FechaAlta
- Email
- Legajo
- MateriasCursadaActivas
- CalificacionesRealizadas
```

**Empleado**
```
- Nombre
- Apellido
- Telefono
- Direccion
- FechaAlta
- Email
- Legajo
```

**Materia**
```
- Nombre
- CodigoMateria
- Descripcion
- CupoMaximo
- MateriasCursadas
- Calificaciones
- Carrera
```

**MateriaCursada**
```
- Nombre
- Anio
- Cuatrimestre
- Activo
- Materia
- Profesor
- Alumnos
- Calificaciones
```

**Carrera**
```
- Nombre
- Materias
- Alumnos
```

**Calificacion**
```
- NotaFinal
- Materia
- MateriaCursada
- Profesor
- Alumno
```

**NOTA:** aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades ⌨️
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

**Usuario**
- Los alumnos pueden auto registrarse, pero quedarán inactivos, hasta que un empleado los active.
-- Que un alumno esté inactivo, significa que no podrá matricularse para cursar las materias.
- La autoregistración desde el sitio, es exclusiva pra los alumnos. Por lo cual, se le asignará dicho rol.
- Los profesores y empleados, deben ser agregados por un empleado.
	- Al momento, del alta del profesor y/o empleado, se le definirá un username y password.
    - También se le asignará a estas cuentas el rol según corresponda.

**Alumno**
- Un alumno puede sacar registrarse para cursar materias de forma Online
    - El alumno, puede matricularse en hasta 5 materias.
    - Las materias en las cuales puede anotarse, deben ser de la materia que cursa el alumno y no debe tenerla activa o ya haberla cursado.
- El alumno puede ver las materias que ya cursó.
    - Puede ver de dichas materias la nota obtenida.
- El alumno puede ver las materias que está inscripto/cursando.
    - El alumno puede cancelar la inscripción a una materia en cualquier momento, pero no debe tener una calificación asociada. En dicho caso, no podrá darse de baja.
    - En el detalle de la materia cursada, el alumno, puede ver un listado de sus compañeros, con Nombre, Apellido y el correo electronico. 
- No puede actualizar datos de contacto, solo puede hacerlo un empleado.

**Profesor**
- El profesor puede listar las materias cursadas, que se le han asignado vigentes y pasadas.
    - En cada una, podrá ver los alumnos.
    - Por cada alumno, podrá realizar una calificación, en tanto y cuanto esté vigente.
        - Las calificaciones posibles serán del 0 a 10 o A (Ausente).
        - Solo el profesor titulas, podrá hacer la calificación y quedará registro del mismo.
    - Por cada materia cursada, el profesor podrá ver un promedio de las notas de los alumnos.

**Empleado**
- Un Empleado, puede crear más empleados, profesores y alumnos.
- Puede crear Carreras, Materias.
- No puede calificar.
- Dar de alta a profesionales, con su horario de atención, y todos los datos requeridos.
- Solo un empleado puede modificar la asignación de un profesor a una cursada.

**Materia y MateriaCursada**
- No existen correlatividades entre las materias.
- La materia debe pertenecer a una carrera.
   - En el caso de que exista una misma materia en más de una carrera deberá crearse una nueva. No hay materias Cross-Carrera.
- Las materias tendrán un cupo máximo de alumnos.
    - En caso de que se alcance el cupo máximo, se deberá generar un nuevo "curso". Ejemplo Si el limite es 10, y existen 10 alumnos registrados en NT1-A-2020-1er Cuatrimestre, el alumno 11 al registrarse, se registrará en NT1-B-2020-1er Cuatrimestre
- Se asume que los profesores pueden dar más de una materia, no hay restricción de día, horario, carga horaria, etc.
    - Por consiguiente, la asignación del profesor en la creación de un nuevo curso automático, será el mismo, del curso previo. Ej. El profesor del curso A, se le asignará al curso B.
    - Un empleado y solo este, podrá modificar la asignación de un profesor a una materia cursada.


**Aplicación General**
- Información institucional.
- Se listarán las carreras y materias por carrera.
- Se listarán profesores de la institución.

`
Nota: El centro tiene consultorios ilimitados y cada consultorio está preparado para soportar cualquier prestación, por lo cual esto no implica restricciones.
`
