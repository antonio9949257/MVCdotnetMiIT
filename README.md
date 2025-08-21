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

## Flujo Específico de Módulos (Curso, Periodo, Sección)

Dado que los controladores `CursoController`, `PeriodoController` y `SeccionController` son idénticos en su estructura actual, podemos usar el flujo de **Curso** como ejemplo representativo.

El objetivo de estos módulos es permitir operaciones CRUD (Crear, Leer, Actualizar, Borrar) sobre las entidades que representan.

**Flujo Típico para Listar Entidades (Ej: Listar Cursos):**

1.  **Petición del Usuario**: El usuario hace clic en un enlace o navega directamente a la URL `/Curso` o `/Curso/Index`.

2.  **Enrutamiento (Routing)**: El sistema de enrutamiento de ASP.NET Core analiza la URL y determina que debe ser manejada por el método `Index()` del controlador `CursoController`.

3.  **Ejecución del Controlador (`CursoController.cs`)**:
    *   Se ejecuta el método `public IActionResult Index()`.
    *   **Lógica de Negocio (Conceptual)**: Dentro de este método, el controlador normalmente se comunicaría con una capa de servicio o directamente con un contexto de base de datos (usando Entity Framework Core, por ejemplo) para solicitar una lista de todos los cursos.
    *   **Preparación de Datos**: Los datos obtenidos se empaquetarían en un Modelo (o ViewModel) si fuera necesario.
    *   **Selección de la Vista**: El método finaliza con `return View();`. Esto le indica al motor de Razor que debe renderizar la vista asociada a esta acción. Por convención, buscará el archivo `Index.cshtml` dentro de la carpeta `Views/Curso/`. Si se pasara un modelo a la vista (ej: `return View(listaDeCursos);`), esos datos estarían disponibles en el archivo `.cshtml`.

4.  **Renderizado de la Vista (`Views/Curso/Index.cshtml`)**:
    *   El archivo `Index.cshtml` contiene el código HTML y Razor para presentar los datos.
    *   Normalmente, aquí habría un bucle (como `@foreach`) que itera sobre la lista de cursos y genera una fila en una tabla HTML para cada uno, mostrando sus propiedades (nombre, créditos, etc.).
    *   También incluiría enlaces para las acciones de "Crear Nuevo", "Editar" o "Eliminar" para cada curso.

5.  **Respuesta al Cliente**: El servidor envía el documento HTML completamente renderizado de vuelta al navegador del usuario, que lo muestra en la pantalla.

Este mismo flujo se aplica de manera idéntica para `Periodo` y `Seccion`, donde cada controlador interactúa con sus vistas y modelos correspondientes.
