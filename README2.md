# Laboratorio: Serialización de Datos en ASP.NET Core MVC

## Objetivo del Laboratorio

El objetivo de este laboratorio es demostrar el proceso de pasar una lista de objetos desde un controlador a una vista utilizando serialización JSON en una aplicación ASP.NET Core MVC. Los pasos principales son:

1.  **Crear un Modelo:** Definir una clase C# que represente la estructura de los datos (en este caso, un curso).
2.  **Poblar Datos en el Controlador:** Crear una lista de objetos del modelo en un método de acción del controlador.
3.  **Serializar a JSON:** Convertir la lista de objetos C# a formato JSON.
4.  **Devolver JSON desde el Controlador:** Utilizar un `JsonResult` para enviar los datos JSON al cliente.
5.  **Consumir JSON en la Vista:** Usar JavaScript (con jQuery y AJAX) en la vista para solicitar los datos JSON y construir dinámicamente una tabla HTML para mostrarlos al usuario.
6.  **Integrar la Navegación:** Añadir un enlace en el menú principal de la aplicación para acceder a la nueva vista.

---

## Implementación Técnica

A continuación se detalla el proceso técnico seguido para implementar la solución.

### 1. Creación del Modelo (`Curso.cs`)

Se creó una clase `Curso` en el directorio `Models/` para representar las entidades del curso. Esta clase funciona como un **DTO (Data Transfer Object)** simple.

```csharp
// /Models/Curso.cs
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

- **`namespace MiIT.Models`**: Asegura que la clase esté organizada dentro del espacio de nombres de los modelos de la aplicación.
- **`public class Curso`**: Define la estructura de datos con propiedades que corresponden a los atributos de un curso.

### 2. Modificación del Controlador (`CursoController.cs`)

Se modificó el controlador `CursoController` para servir los datos de los cursos.

```csharp
// /Controllers/CursoController.cs
using Microsoft.AspNetCore.Mvc;
using MiIT.Models; // Se importa el namespace del modelo

namespace MiIT.Controllers
{
    public class CursoController : Controller
    {
        // ... (método Index existente)

        // Nuevo método para devolver datos en formato JSON
        public JsonResult CursosJson()
        {
            // 1. Se instancia una lista de objetos del modelo Curso
            var cursos = new List<Curso>
            {
                new Curso { idCurso = 1, nombre = "CAI-500 CONTROL Y AUTOMATIZACIÓN INDUSTRIAL II", ... },
                new Curso { idCurso = 2, nombre = "TEW-500 TECNOLOGÍA WEB II", ... },
                // ... (resto de los cursos)
            };

            // 2. Se serializa la lista a JSON y se devuelve como un JsonResult
            return Json(cursos);
        }
    }
}
```

- **`CursosJson()`**: Es un nuevo método de acción que no devuelve una vista (`ViewResult`), sino un `JsonResult`.
- **`List<Curso>`**: Se crea una colección genérica de `List` que almacena instancias del modelo `Curso`, representando las asignaturas del semestre.
- **`return Json(cursos)`**: Este es el paso clave de la serialización. El método `Json()` de la clase base `Controller` toma el objeto C# (`List<Curso>`), lo serializa a una cadena en formato JSON y lo encapsula en un `JsonResult`, que se envía al cliente con el tipo de contenido `application/json`.

### 3. Implementación de la Vista (`Index.cshtml`)

La vista `Views/Curso/Index.cshtml` fue modificada para renderizar una tabla vacía y luego llenarla dinámicamente usando JavaScript.

```html
<!-- /Views/Curso/Index.cshtml -->
<h1>Cursos</h1>

<!-- Estructura base de la tabla HTML -->
<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Descripción</th>
            <th>Habilitado</th>
        </tr>
    </thead>
    <tbody id="cursos-tbody">
        <!-- El contenido se generará dinámicamente aquí -->
    </tbody>
</table>

<!-- La sección de Scripts se renderiza al final del body en _Layout.cshtml -->
@section Scripts {
    <script>
        // Se ejecuta cuando el DOM está completamente cargado
        $(document).ready(function () {
            // 1. Petición AJAX con jQuery
            $.ajax({
                url: "/Curso/CursosJson", // Endpoint del controlador que devuelve JSON
                type: "GET",
                dataType: "json",
                success: function (data) { // 2. Callback que se ejecuta si la petición es exitosa
                    var tbody = $("#cursos-tbody");
                    tbody.empty(); // Limpia el cuerpo de la tabla por si tuviera datos previos

                    // 3. Itera sobre el array JSON recibido
                    $.each(data, function (i, item) {
                        // 4. Construye una fila de tabla (<tr>) por cada objeto en el array
                        var row = "<tr>";
                        row += "<td>" + item.idCurso + "</td>";
                        row += "<td>" + item.nombre + "</td>";
                        row += "<td>" + item.descripcion + "</td>";
                        row += "<td>" + item.bhabilitado + "</td>";
                        row += "</tr>";
                        tbody.append(row); // 5. Añade la fila al cuerpo de la tabla
                    });
                }
            });
        });
    </script>
}
```

- **`@section Scripts`**: Directiva de Razor que permite inyectar contenido en una sección definida en la plantilla principal (`_Layout.cshtml`). Es la forma correcta de añadir scripts específicos de una vista.
- **`$(document).ready()`**: Función de jQuery que asegura que el script se ejecute solo después de que la página se haya cargado por completo.
- **`$.ajax()`**: Realiza una petición asíncrona (AJAX) al servidor.
  - **`url: "/Curso/CursosJson"`**: Especifica la URL del método de acción en el controlador. ASP.NET Core MVC enruta esta petición al método `CursosJson` del `CursoController`.
  - **`type: "GET"`**: Es el método HTTP utilizado para la solicitud.
  - **`dataType: "json"`**: Le indica a jQuery que espere una respuesta en formato JSON, y la parseará automáticamente a un objeto JavaScript.
- **`success: function (data)`**: Es la función que se ejecuta cuando el servidor responde con un código 200 (OK). El parámetro `data` contiene el array de objetos JSON (ya parseado) devuelto por el controlador.
- **`$.each(data, ...)`**: Se itera sobre cada elemento del array `data`.
- **`tbody.append(row)`**: Se utiliza jQuery para manipular el DOM, añadiendo dinámicamente cada nueva fila (`<tr>`) al `<tbody>` de la tabla.

### 4. Actualización de la Navegación (`_Layout.cshtml`)

Finalmente, se añadió un nuevo elemento a la lista de navegación en la plantilla principal para que los usuarios puedan acceder a la página de cursos.

```html
<!-- /Views/Shared/_Layout.cshtml -->
<ul class="navbar-nav flex-grow-1">
    ...
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Curso" asp-action="Index">Cursos</a>
    </li>
    ...
</ul>
```

- **`asp-controller="Curso"`**: Tag Helper de ASP.NET que genera el `href` apuntando al controlador `CursoController`.
- **`asp-action="Index"`**: Tag Helper que apunta al método de acción `Index` dentro de ese controlador.

Este enfoque desacopla la presentación (HTML/CSS) de los datos, permitiendo que la vista solicite y muestre información de forma asíncrona sin necesidad de recargar la página completa.
