# Instituto Educativo 馃摉

## Objetivos 馃搵
Desarrollar un sistema, que permita la administraci贸n general de un Instituto Educativo(de cara a los Empleados): Profesores, Alumnos, Materias, Cursos, Calificaciones, Carreras, etc., como as铆 tambi茅n, permitir a los Profesores, realizar calificaciones y de cara a los Alumnos matricularse en las materias pendientes.
Utilizar Visual Studio 2019 preferentemente y crear una aplicaci贸n utilizando ASP.NET MVC Core (versi贸n a de finir por el docente 2.2 o 3.1).

<hr />

## Enunciado 馃摙
La idea principal de este trabajo pr谩ctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, el cual relev贸 e identific贸 la informaci贸n aqu铆 contenida. 
A partir de este momento, deber谩n comprender lo que se est谩 requiriendo y construir dicha aplicaci贸n, 

Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, 茅l nos ayudar谩 a conseguir la informaci贸n ya un poco m谩s procesada. 
Es importante destacar, que este proceso, no debe esperar a ser en clase; es importante, que junten algunas consultas, sea de 铆ndole funcional o t茅cnicas, en lugar de cada consulta enviarla de forma independiente.

Las consultas que sean realizadas por correo deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>

2.< xxxxxxxx>


# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relaci贸n del paciente con Turno es 1:1 o 1:N?

2.Est谩 bien que encaremos la validaci贸n del turno activo, con una propiedad booleana en el Turno?

<hr />

### Proceso de ejecuci贸n en alto nivel 鈽戯笍
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Crear las relaciones entre las entidades
 - Crear una carpeta Data que dentro tendr谩 al menos la clase que representar谩 el contexto de la base de datos DbContext. 
 - Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades en el DbContext.
 - Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Realizar un sistema de login con al menos los roles equivalentes a <Usuario Cliente> y <Usuario Administrador> (o con permisos elevados).
 - Si el proyecto lo requiere, generar el proceso de registraci贸n. 
 - Un administrador podr谩 realizar todas tareas que impliquen interacci贸n del lado del negocio (ABM "Alta-Baja-Modificaci贸n" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente> s贸lo podr谩 tomar acci贸n en el sistema, en base al rol que tiene.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos del lado de los permisos.
 - Todo lo referido a la presentaci贸n de la aplicai贸n (cuestiones visuales).
 
<hr />

## Entidades 馃搫

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
Las propiedades descriptas a continuaci贸n, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como as铆 tambi茅n las restricciones.
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

**NOTA:** aqu铆 un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades 鈱笍
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

**Usuario**
- Los alumnos pueden auto registrarse, pero quedar谩n inactivos, hasta que un empleado los active.
-- Que un alumno est茅 inactivo, significa que no podr谩 matricularse para cursar las materias.
- La autoregistraci贸n desde el sitio, es exclusiva pra los alumnos. Por lo cual, se le asignar谩 dicho rol.
- Los profesores y empleados, deben ser agregados por un empleado.
	- Al momento, del alta del profesor y/o empleado, se le definir谩 un username y password.
    - Tambi茅n se le asignar谩 a estas cuentas el rol seg煤n corresponda.

**Alumno**
- Un alumno puede sacar registrarse para cursar materias de forma Online
    - El alumno, puede matricularse en hasta 5 materias.
    - Las materias en las cuales puede anotarse, deben ser de la materia que cursa el alumno y no debe tenerla activa o ya haberla cursado.
- El alumno puede ver las materias que ya curs贸.
    - Puede ver de dichas materias la nota obtenida.
- El alumno puede ver las materias que est谩 inscripto/cursando.
    - El alumno puede cancelar la inscripci贸n a una materia en cualquier momento, pero no debe tener una calificaci贸n asociada. En dicho caso, no podr谩 darse de baja.
    - En el detalle de la materia cursada, el alumno, puede ver un listado de sus compa帽eros, con Nombre, Apellido y el correo electronico. 
- No puede actualizar datos de contacto, solo puede hacerlo un empleado.

**Profesor**
- El profesor puede listar las materias cursadas, que se le han asignado vigentes y pasadas.
    - En cada una, podr谩 ver los alumnos.
    - Por cada alumno, podr谩 realizar una calificaci贸n, en tanto y cuanto est茅 vigente.
        - Las calificaciones posibles ser谩n del 0 a 10 o A (Ausente).
        - Solo el profesor titulas, podr谩 hacer la calificaci贸n y quedar谩 registro del mismo.
    - Por cada materia cursada, el profesor podr谩 ver un promedio de las notas de los alumnos.

**Empleado**
- Un Empleado, puede crear m谩s empleados, profesores y alumnos.
- Puede crear Carreras, Materias.
- No puede calificar.
- Dar de alta a profesionales, con su horario de atenci贸n, y todos los datos requeridos.
- Solo un empleado puede modificar la asignaci贸n de un profesor a una cursada.

**Materia y MateriaCursada**
- No existen correlatividades entre las materias.
- La materia debe pertenecer a una carrera.
   - En el caso de que exista una misma materia en m谩s de una carrera deber谩 crearse una nueva. No hay materias Cross-Carrera.
- Las materias tendr谩n un cupo m谩ximo de alumnos.
    - En caso de que se alcance el cupo m谩ximo, se deber谩 generar un nuevo "curso". Ejemplo Si el limite es 10, y existen 10 alumnos registrados en NT1-A-2020-1er Cuatrimestre, el alumno 11 al registrarse, se registrar谩 en NT1-B-2020-1er Cuatrimestre
- Se asume que los profesores pueden dar m谩s de una materia, no hay restricci贸n de d铆a, horario, carga horaria, etc.
    - Por consiguiente, la asignaci贸n del profesor en la creaci贸n de un nuevo curso autom谩tico, ser谩 el mismo, del curso previo. Ej. El profesor del curso A, se le asignar谩 al curso B.
    - Un empleado y solo este, podr谩 modificar la asignaci贸n de un profesor a una materia cursada.


**Aplicaci贸n General**
- Informaci贸n institucional.
- Se listar谩n las carreras y materias por carrera.
- Se listar谩n profesores de la instituci贸n.

`
Nota: El centro tiene consultorios ilimitados y cada consultorio est谩 preparado para soportar cualquier prestaci贸n, por lo cual esto no implica restricciones.
`
