# Laboratorio 7: Conexión a Base de Datos con Entity Framework Core

## Objetivo del Laboratorio

El objetivo de este laboratorio es evolucionar la aplicación web para que consuma datos desde una base de datos real en lugar de una lista estática codificada en el controlador. Para lograr esto, se utiliza **Entity Framework Core (EF Core)**, un mapeador objeto-relacional (O/RM) que simplifica el acceso a datos en aplicaciones .NET.

Los pasos clave son:

1.  **Configurar el Entorno:** Preparar la base de datos y añadir las dependencias de EF Core al proyecto.
2.  **Establecer la Conexión:** Definir la cadena de conexión en `appsettings.json`.
3.  **Crear el Contexto de Datos:** Implementar una clase `DbContext` que representa la sesión con la base de datos y especifica qué tablas serán consultadas.
4.  **Registrar Servicios:** Configurar la inyección de dependencias para que la aplicación pueda usar el `DbContext`.
5.  **Refactorizar el Controlador:** Modificar el `CursoController` para que use el `DbContext` para consultar los datos de los cursos directamente desde la base de datos.

---

## Implementación Técnica

A continuación se detalla el proceso técnico seguido para conectar la aplicación a la base de datos `matriculas`.

### 1. Prerrequisitos: Base de Datos y Paquetes

- **Base de Datos:** Se asume que el usuario ha restaurado una base de datos llamada `matriculas` en una instancia de SQL Server.
- **Entity Framework Core:** Se añadió el proveedor de EF Core para SQL Server al proyecto mediante el siguiente comando de la CLI de .NET:

  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  ```

  Este paquete permite a EF Core comunicarse con una base de datos SQL Server.

### 2. Configuración de la Cadena de Conexión (`appsettings.json`)

Para evitar codificar datos sensibles en el código, la información de conexión a la base de datos se almacena en `appsettings.json`. Se añadió una nueva sección `ConnectionStrings`.

```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=matriculas;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  // ... resto de la configuración
}
```

- **`DefaultConnection`**: Es el nombre que le damos a nuestra cadena de conexión.
- **`Server=TU_SERVIDOR`**: **Este valor debe ser reemplazado por el usuario** con el nombre de su instancia de SQL Server (ej. `localhost`, `.\SQLEXPRESS`).
- **`Database=matriculas`**: Especifica el nombre de la base de datos a la que nos conectaremos.
- **`Trusted_Connection=True`**: Indica que se usará la autenticación de Windows. 
- **`TrustServerCertificate=True`**: Necesario en conexiones locales para confiar en el certificado del servidor SQL.

### 3. Creación del Contexto de la Base de Datos (`MatriculasContext.cs`)

El `DbContext` es el corazón de EF Core. Actúa como un puente entre el código C# (los modelos) y la base de datos.

Se creó el archivo `Models/MatriculasContext.cs`:

```csharp
// /Models/MatriculasContext.cs
using Microsoft.EntityFrameworkCore;
using MiIT.Models;

namespace MiIT.Data
{
    public class MatriculasContext : DbContext
    {
        public MatriculasContext(DbContextOptions<MatriculasContext> options) : base(options) {}

        // Mapea la clase modelo "Curso" a la tabla "Curso" en la BD
        public DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asegura que EF Core busque una tabla llamada "Curso"
            modelBuilder.Entity<Curso>().ToTable("Curso");
        }
    }
}
```

- **`MatriculasContext : DbContext`**: La clase hereda de `DbContext` de EF Core.
- **`public DbSet<Curso> Curso { get; set; }`**: Esta propiedad `DbSet` le dice a EF Core que hay una tabla que corresponde al modelo `Curso`. A través de esta propiedad se realizarán las consultas (lectura, escritura, actualización, etc.) a la tabla `Curso`.

### 4. Registro del DbContext en `Program.cs`

Para que el resto de la aplicación (como los controladores) pueda recibir una instancia de `MatriculasContext` a través de la inyección de dependencias, debe registrarse como un servicio en `Program.cs`.

```csharp
// Program.cs
using Microsoft.EntityFrameworkCore;
using MiIT.Data;

var builder = WebApplication.CreateBuilder(args);

// Se añade el DbContext al contenedor de servicios
builder.Services.AddDbContext<MatriculasContext>(options =>
    // Se configura para usar SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ... resto de los servicios
```

- **`builder.Services.AddDbContext<...>()`**: Registra el `MatriculasContext` en el sistema de inyección de dependencias.
- **`options.UseSqlServer(...)`**: Especifica que este contexto usará el proveedor de SQL Server.
- **`builder.Configuration.GetConnectionString("DefaultConnection")`**: Lee la cadena de conexión desde `appsettings.json`, desacoplando la configuración del código.

### 5. Refactorización del Controlador (`CursoController.cs`)

Finalmente, se modificó el `CursoController` para que, en lugar de crear una lista manual, consulte la base de datos a través del `DbContext` inyectado.

```csharp
// /Controllers/CursoController.cs
using Microsoft.AspNetCore.Mvc;
using MiIT.Data; // Namespace del DbContext
using MiIT.Models;

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        // 1. Campo privado para almacenar la instancia del DbContext
        private readonly MatriculasContext _context;

        // 2. Inyección de dependencias a través del constructor
        public CursoController(MatriculasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult CursosJson()
        {
            // 3. Se consulta la base de datos usando LINQ y EF Core
            var cursos = _context.Curso.ToList();
            return Json(cursos);
        }
    }
}
```

- **Inyección de Dependencias**: El `MatriculasContext` se "inyecta" en el constructor del controlador. El sistema de servicios de ASP.NET Core se encarga de crear y pasar esta instancia automáticamente.
- **Consulta a la Base de Datos**: La línea `var cursos = _context.Curso.ToList();` es donde ocurre la magia. 
  - `_context.Curso` se refiere al `DbSet` que representa la tabla de cursos.
  - El método `.ToList()` ejecuta la consulta contra la base de datos (traduciéndose a un `SELECT * FROM Curso` en SQL).
  - EF Core mapea automáticamente los resultados de la consulta a una `List<Curso>` de objetos C#.
- El resto del flujo permanece igual: la lista (ahora obtenida de la base de datos) se serializa a JSON y se envía a la vista, que la renderiza de forma asíncrona como en el laboratorio anterior.
