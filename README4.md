# Laboratorio 7: Corrección de Errores de Mapeo y Configuración en Entity Framework Core

## Objetivo

Este documento detalla los pasos técnicos realizados para solucionar una serie de errores de configuración y mapeo de Entity Framework Core (EF Core) que impedían que la aplicación se conectara y consultara correctamente una base de datos SQL Server restaurada.

---

## Paso 1: Ajuste de la Cadena de Conexión

**Problema:** La cadena de conexión en `appsettings.json` apuntaba a una base de datos con un nombre incorrecto (`matriculas`) en lugar de la base de datos restaurada (`SISTEMAMATRICULA`).

**Solución:** Se modificó el valor del parámetro `Database` en la cadena de conexión `DefaultConnection`.

**Archivo modificado:** `appsettings.json`

**Código anterior:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=matriculas;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
  // ...
}
```

**Código posterior:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=SISTEMAMATRICULA;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
  // ...
}
```

---

## Paso 2: Definición de la Clave Primaria (Primary Key)

**Problema:** Al ejecutar la aplicación, EF Core arrojó una excepción indicando que la entidad `Curso` no tenía una clave primaria definida (`The entity type 'Curso' requires a primary key to be defined`).

**Solución:** Se utilizó la anotación de datos `[Key]` para designar explícitamente la propiedad `idCurso` como la clave primaria de la entidad `Curso`.

**Archivo modificado:** `Models/Curso.cs`

**Código anterior:**
```csharp
namespace MiIT.Models
{
    public class Curso
    {
        public int idCurso { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public bool bhabilitado { get; set; }
    }
}
```

**Código posterior:**
```csharp
using System.ComponentModel.DataAnnotations;

namespace MiIT.Models
{
    public class Curso
    {
        [Key]
        public int idCurso { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public bool bhabilitado { get; set; }
    }
}
```

---

## Paso 3: Mapeo de Columnas y Propiedades (Column Mapping)

**Problema:** Tras solucionar el primer error, surgió una nueva excepción (`Invalid column name 'idCurso'`). Esto se debió a un desajuste entre los nombres de las propiedades del modelo C# (ej. `idCurso`, `nombre`) y los nombres de las columnas en la tabla de la base de datos (ej. `IIDCURSO`, `NOMBRE`).

**Solución:** Se realizaron los siguientes ajustes en el modelo `Curso.cs`:
1.  Se adoptó la convención de nomenclatura `PascalCase` para las propiedades de C# (ej. `IdCurso`).
2.  Se utilizó la anotación `[Column("...")]` para mapear cada propiedad a su nombre de columna correspondiente en la base de datos.
3.  Se ajustó el tipo de la propiedad `Bhabilitado` de `bool` a `int` para que coincidiera con el tipo de la columna en la base de datos.

**Archivo modificado:** `Models/Curso.cs`

**Código posterior (final):**
```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiIT.Models
{
    public class Curso
    {
        [Key]
        [Column("IIDCURSO")]
        public int IdCurso { get; set; }

        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string? Descripcion { get; set; }

        [Column("BHABILITADO")]
        public int Bhabilitado { get; set; }
    }
}
```

---

## Paso 4: Preservar el Contrato de la API JSON

**Problema:** El cambio de nombres de propiedades en el modelo (de `idCurso` a `IdCurso`) provocaría que el JSON generado por la API cambiara sus campos a `PascalCase` (ej. `"IdCurso": 1`), lo que podría romper la integración con cualquier cliente frontend que esperara `camelCase` (ej. `"idCurso": 1`).

**Solución:** Se modificó el método `CursosJson` en `CursoController.cs` para configurar explícitamente el serializador JSON, forzándolo a usar `JsonNamingPolicy.CamelCase` y manteniendo así el contrato original de la API.

**Archivo modificado:** `Controllers/CursoController.cs`

**Código anterior:**
```csharp
public JsonResult CursosJson()
{
    var cursos = _context.Curso.ToList();
    return Json(cursos);
}
```

**Código posterior:**
```csharp
using System.Text.Json;

// ...

public JsonResult CursosJson()
{
    var cursos = _context.Curso.ToList();
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    return Json(cursos, options);
}
```

## Conclusión

Con estas cuatro intervenciones, la aplicación quedó correctamente configurada para comunicarse con la base de datos, resolver los desajustes de mapeo de EF Core y exponer los datos a través de la API de forma consistente y sin errores.
