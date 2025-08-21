# MiIT Project

Este es un proyecto web desarrollado con ASP.NET Core MVC.

## Descripción

La aplicación parece ser un sistema de gestión académica o escolar, con funcionalidades para manejar Cursos, Periodos y Secciones.

## Requisitos Previos

*   [.NET SDK](https://dotnet.microsoft.com/download) (la versión dependerá de la especificada en el archivo `.csproj`)
*   Un editor de código como Visual Studio Code o JetBrains Rider.

## Instalación y Ejecución en Linux

Sigue estos pasos para poner en marcha el proyecto en un entorno Linux:

1.  **Clonar el repositorio (si está en uno):**
    ```bash
    # git clone <url-del-repositorio>
    # cd MiIT
    ```

2.  **Restaurar dependencias:**
    Este comando descarga todas las dependencias de NuGet necesarias para el proyecto.
    ```bash
    dotnet restore
    ```

3.  **Construir el proyecto:**
    Este comando compila todo el código fuente del proyecto.
    ```bash
    dotnet build
    ```

4.  **Ejecutar la aplicación:**
    Este comando inicia el servidor web y ejecuta la aplicación. Por defecto, será accesible en `http://localhost:5000` o `https://localhost:5001`.
    ```bash
    dotnet run
    ```

## Flujo del Código y Estructura del Proyecto

Esta aplicación sigue el patrón de diseño **Modelo-Vista-Controlador (MVC)**, que separa las responsabilidades de la aplicación en tres partes interconectadas.

### 1. Punto de Entrada (`Program.cs`)

*   La ejecución de la aplicación comienza en el archivo `Program.cs`.
*   Este archivo configura y lanza el host del servidor web Kestrel.
*   Define la configuración inicial, como los servicios que se usarán (inyección de dependencias) y el pipeline de peticiones HTTP (middlewares).

### 2. Configuración (`appsettings.json`)

*   Contiene la configuración de la aplicación, como cadenas de conexión a la base de datos, claves de API, etc.

### 3. Patrón MVC

*   **Controladores (`Controllers/`)**:
    *   Gestionan las peticiones HTTP entrantes del navegador.
    *   Contienen la lógica principal de la aplicación. Por ejemplo, `CursoController.cs` maneja las peticiones relacionadas con los cursos (crear, listar, etc.).
    *   Interactúan con los modelos para obtener o guardar datos y luego seleccionan una vista para devolver una respuesta al usuario.

*   **Modelos (`Models/`)**:
    *   Representan los datos de la aplicación.
    *   Pueden ser clases simples (POCOs) que mapean a tablas de una base de datos o ViewModels (como `ErrorViewModel.cs`) que preparan datos específicamente para una vista.

*   **Vistas (`Views/`)**:
    *   Son los archivos `.cshtml` (Razor) que generan el HTML que se envía al navegador.
    *   La estructura de carpetas en `Views/` coincide con los nombres de los controladores. Por ejemplo, las vistas para `CursoController` están en la carpeta `Views/Curso/`.
    *   `Views/Shared/` contiene vistas reutilizables, como la plantilla principal (`_Layout.cshtml`) y vistas parciales.

### 4. Recursos Estáticos (`wwwroot/`)

*   Esta carpeta contiene todos los archivos estáticos que se sirven directamente al cliente (navegador).
*   **`css/`**: Hojas de estilo CSS, como `site.css`.
*   **`js/`**: Archivos de JavaScript, como `site.js`.
*   **`lib/`**: Bibliotecas de frontend de terceros (como Bootstrap y jQuery), gestionadas a través de `libman.json`.

### 5. Gestión de Bibliotecas Frontend (`libman.json`)

*   Este archivo se usa para definir y restaurar bibliotecas del lado del cliente (CSS/JS) sin necesidad de usar un gestor de paquetes como npm.

## Carga de Datos Asíncrona con JsonResult y jQuery

Para mejorar la interactividad y desacoplar la carga de datos de la renderización inicial de la página, se ha implementado un patrón de carga de datos asíncrona. En lugar de pasar los datos directamente desde la acción `Index` a la vista, la vista se carga inicialmente vacía y luego utiliza una llamada de jQuery AJAX para solicitar los datos a una acción del controlador que devuelve un `JsonResult`.

Este enfoque tiene varias ventajas:

*   **Mejora el tiempo de carga inicial de la página.**
*   **Permite actualizar los datos sin recargar la página completa.**
*   **Separa claramente la lógica de obtención de datos (backend) de la lógica de presentación (frontend).**

### Flujo de la Carga Asíncrona

1.  **Petición Inicial**: El usuario navega a una URL como `/Curso`.
2.  **Controlador (Acción `Index`)**: El método `Index()` del controlador (`CursoController` en este caso) se ejecuta, pero no realiza ninguna consulta de datos. Simplemente devuelve la vista principal.
    ```csharp
    public IActionResult Index()
    {
        return View();
    }
    ```
3.  **Vista (Renderizado Inicial)**: La vista (`Views/Curso/Index.cshtml`) se renderiza con una estructura HTML básica (por ejemplo, una lista `<ul>` o una tabla `<table>` vacía) que servirá como contenedor para los datos.
4.  **Script de jQuery (AJAX Call)**: Una vez que el documento está listo (`$(document).ready()`), un script de JavaScript ejecuta una petición AJAX (`$.ajax`) a una acción específica del controlador (por ejemplo, `GetCursos`).
5.  **Controlador (Acción `JsonResult`)**: La acción `GetCursos()` se ejecuta. Esta acción sí consulta la base de datos (o en este caso, genera datos de ejemplo) y los serializa en formato JSON.
    ```csharp
    [HttpGet]
    public JsonResult GetCursos()
    {
        var cursos = new List<object> { /* ... lista de cursos ... */ };
        return Json(cursos);
    }
    ```
6.  **Respuesta y Manipulación del DOM**: El script de jQuery recibe la respuesta JSON. En la función `success`, itera sobre los datos y los utiliza para construir dinámicamente el contenido HTML (por ejemplo, elementos `<li>` o filas `<tr>`) y lo inyecta en el contenedor que se dejó vacío en el paso 3.

### Ejemplos de Implementación

#### Módulo de Cursos

*   **`Controllers/CursoController.cs`**

    ```csharp
    [HttpGet]
    public JsonResult GetCursos()
    {
        var cursos = new List<object>
        {
            new { Codigo = "CAI-500", Nombre = "CONTROL Y AUTOMATIZACIÓN INDUSTRIAL II" },
            new { Codigo = "TEW-500", Nombre = "TECNOLOGÍA WEB II" },
            new { Codigo = "SII-500", Nombre = "SISTEMAS DE INFORMACIÓN II" },
            new { Codigo = "TEL-500", Nombre = "TELEMÁTICA II" },
            new { Codigo = "EMP-500", Nombre = "EMPRENDIMIENTO PRODUCTIVO I" },
            new { Codigo = "TMG-500", Nombre = "TALLER DE MODALIDAD DE GRADUACIÓN I" }
        };
        return Json(cursos);
    }
    ```

*   **`Views/Curso/Index.cshtml` (Script)**

    ```html
    @section Scripts {
        <script>
            $(document).ready(function () {
                $.ajax({
                    url: '@Url.Action("GetCursos", "Curso")',
                    type: 'GET',
                    dataType: 'json',
                    success: function (data) {
                        var list = $('#cursos-list');
                        list.empty();
                        $.each(data, function (i, curso) {
                            var item = '<li>' + curso.codigo + ' ' + curso.nombre + '</li>';
                            list.append(item);
                        });
                    }
                });
            });
        </script>
    }
    ```

#### Módulo de Periodos

*   **`Controllers/PeridoController.cs`**

    ```csharp
    [HttpGet]
    public JsonResult GetPeriodos()
    {
        var periodos = new List<object>
        {
            new { Semestre = "Primer Semestre", Materias = new List<object> { /* ... */ } },
            new { Semestre = "Segundo Semestre", Materias = new List<object> { /* ... */ } },
            // ... más semestres
        };
        return Json(periodos);
    }
    ```

*   **`Views/Periodo/Index.cshtml` (Script)**

    ```html
    @section Scripts {
        <script>
            $(document).ready(function () {
                $.ajax({
                    url: '@Url.Action("GetPeriodos", "Periodo")',
                    type: 'GET',
                    dataType: 'json',
                    success: function (data) {
                        var container = $('#historial-academico');
                        container.empty();
                        $.each(data, function (i, semestre) {
                            // ... lógica para construir el HTML del historial
                        });
                    }
                });
            });
        </script>
    }
    ```

#### Módulo de Sección (Datos Personales)

*   **`Controllers/SeccionController.cs`**

    ```csharp
    [HttpGet]
    public JsonResult GetDatosPersonales()

    {
        var datos = new { Nombre = "Armin Daniel Antonio Mendieta", Cedula = "9949257", /* ... */ };
        return Json(datos);
    }
    ```

*   **`Views/Seccion/Index.cshtml` (Script)**

    ```html
    @section Scripts {
        <script>
            $(document).ready(function () {
                // Cargar datos personales
                $.ajax({
                    url: '@Url.Action("GetDatosPersonales", "Seccion")',
                    type: 'GET',
                    dataType: 'json',
                    success: function (data) {
                        var list = $('#datos-personales-list');
                        list.empty();
                        // ... lógica para mostrar datos personales
                    }
                });
            });
        </script>
    }
    ```

## Explicación de Propiedades en C# (Getters y Setters)

En C#, los "getters" y "setters" no se implementan como métodos separados (como en otros lenguajes como Java), sino a través de un miembro de la clase llamado **Propiedad**. Las propiedades proporcionan una forma flexible y segura de leer, escribir o calcular los valores de campos privados, manteniendo el principio de **encapsulación**.

La encapsulación es un pilar de la Programación Orientada a Objetos que consiste en ocultar el estado interno de un objeto y requerir que toda la interacción se realice a través de los métodos (o propiedades) del objeto.

### ¿Cómo Funcionan?

Una propiedad se compone de dos "accesores":

*   **`get`**: Se ejecuta cuando se lee el valor de la propiedad. Debe devolver un valor del mismo tipo que la propiedad.
*   **`set`**: Se ejecuta cuando se asigna un valor a la propiedad. Tiene acceso a un parámetro implícito llamado `value`, que contiene el valor que se está asignando.

### Tipos de Propiedades

#### 1. Propiedades Autoimplementadas (la forma más común)

Son la forma más concisa de declarar una propiedad cuando no se necesita lógica adicional en los descriptores de acceso. El compilador crea automáticamente un campo de respaldo privado y anónimo.

**Ejemplo:**

```csharp
// Se declara un modelo para representar un Curso.
// Este archivo estaría en Models/Curso.cs
public class Curso
{
    // Propiedad autoimplementada para el ID del curso.
    // El compilador crea un campo `private int _id;` de fondo.
    public int Id { get; set; }

    // Propiedad autoimplementada para el Nombre del curso.
    public string Nombre { get; set; }
}
```

**Uso:**

```csharp
Curso miCurso = new Curso();

// Aquí se invoca el accesor `set` de la propiedad Nombre.
// El compilador hace: miCurso._nombre = "Programación I";
miCurso.Nombre = "Programación I"; 

// Aquí se invoca el accesor `get` de la propiedad Nombre.
// El compilador hace: return miCurso._nombre;
string nombreDelCurso = miCurso.Nombre; 

Console.WriteLine(nombreDelCurso); // Imprime "Programación I"
```

#### 2. Propiedades con Campo de Respaldo Explícito

Se usan cuando necesitas añadir lógica personalizada antes de leer o escribir un valor. Para ello, declaras un campo privado (el "campo de respaldo" o "backing field") y luego la propiedad pública que interactúa con él.

**Ejemplo:**

Supongamos que no queremos que el nombre de un curso esté vacío. Podemos añadir una validación en el accesor `set`.

```csharp
public class Curso
{
    // 1. Campo de respaldo privado para almacenar el nombre.
    private string _nombre;

    // 2. Propiedad pública que controla el acceso al campo _nombre.
    public string Nombre
    {
        // Accesor GET: simplemente devuelve el valor del campo de respaldo.
        get
        {
            return _nombre;
        }

        // Accesor SET: se ejecuta cuando se asigna un valor.
        set
        {
            // `value` es la palabra clave que contiene el valor asignado.
            if (string.IsNullOrWhiteSpace(value))
            {
                // Lanzamos una excepción si el nombre es nulo o vacío.
                throw new ArgumentException("El nombre del curso no puede estar vacío.");
            }
            // Si el valor es válido, lo asignamos al campo de respaldo.
            _nombre = value;
        }
    }
}
```

**Uso:**

```csharp
Curso miCurso = new Curso();

miCurso.Nombre = "Bases de Datos"; // Funciona, invoca el `set`.
Console.WriteLine(miCurso.Nombre); // Funciona, invoca el `get`.

// Esto lanzará una ArgumentException debido a la lógica en el `set`.
miCurso.Nombre = ""; 
```

### Control de Acceso

También puedes especificar diferentes niveles de acceso para cada accesor. Por ejemplo, puedes tener una propiedad que se puede leer desde cualquier lugar (`public get`), pero que solo se puede modificar desde dentro de la propia clase (`private set`).

**Ejemplo:**

```csharp
public class Estudiante
{
    // El ID se puede leer desde fuera, pero solo la clase Estudiante
    // puede asignarlo (por ejemplo, en el constructor).
    public int Id { get; private set; }

    public string Nombre { get; set; }

    public Estudiante(int id)
    {
        // El `set` de Id es privado, pero se puede usar aquí.
        this.Id = id;
    }
}
```

En resumen, las propiedades de C# son una característica potente que encapsula la lógica de acceso a los datos de un objeto, permitiendo un código más limpio, seguro y fácil de mantener.

## Cambio a Renderizado de Datos en el Servidor (Server-Side Rendering)

Para simplificar la arquitectura y mejorar el SEO, la carga de datos en los módulos de Cursos, Periodos y Sección ha sido refactorizada para utilizar un enfoque de renderizado en el servidor, eliminando la dependencia de JavaScript (jQuery AJAX) para la carga inicial de datos.

### ¿Cómo Funciona Ahora?

1.  **Modelos de Datos Claros:** Se han introducido clases de modelo específicas (`Curso`, `Materia`, `Periodo`, `DatosPersonales`) en la carpeta `Models/` para representar de forma estructurada los datos que se manejan.
2.  **Controladores (Backend):**
    *   Las acciones `Index()` en `CursoController.cs`, `PeridoController.cs` y `SeccionController.cs` ahora son responsables de obtener directamente los datos necesarios (los mismos datos que antes se devolvían vía `JsonResult`).
    *   Estos datos se empaquetan en una instancia del modelo correspondiente y se pasan directamente a la vista utilizando `return View(modelo);`.
    *   Los métodos `[HttpGet] JsonResult Get...()` que antes servían los datos vía AJAX han sido eliminados.
3.  **Vistas (Frontend):**
    *   Las vistas (`Views/Curso/Index.cshtml`, `Views/Periodo/Index.cshtml`, `Views/Seccion/Index.cshtml`) ahora declaran el tipo de modelo que esperan al inicio (`@model TipoDeModelo`).
    *   Utilizan directamente la sintaxis Razor (`@foreach`, `@Model.Propiedad`) para iterar sobre los datos del modelo y generar el HTML completo en el servidor antes de enviarlo al navegador.
    *   Las secciones `@section Scripts` que contenían las llamadas AJAX de jQuery han sido removidas.

### Ventajas de este Enfoque:

*   **Simplicidad:** Menos código JavaScript para la carga inicial de datos.
*   **SEO Mejorado:** El contenido está disponible directamente en el HTML generado por el servidor, lo que facilita la indexación por parte de los motores de búsqueda.
*   **Menos Dependencias:** Se reduce la dependencia de librerías JavaScript para la carga de datos.

Este cambio asegura que la página se cargue con todos los datos ya presentes, sin necesidad de peticiones adicionales post-carga.