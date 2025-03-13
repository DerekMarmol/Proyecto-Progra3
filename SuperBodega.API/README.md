# API Principal - Super Bodega

Este documento contiene la información necesaria para entender, configurar y utilizar la API Principal del proyecto Super Bodega.

## Tecnologías utilizadas

- ASP.NET Core 6 (C#)
- Entity Framework Core
- SQL Server (en Docker)
- Swagger para documentación
- Docker para contenerización

## Estructura del proyecto

- **Controllers/**: Controladores de la API
- **Models/**: Modelos de entidades
- **Data/**: Contexto de base de datos y repositorios
- **Services/**: Servicios con lógica de negocio
- **DTOs/**: Objetos de transferencia de datos
- **Middlewares/**: Middleware personalizado
- **Validators/**: Validadores de entrada
- **Infrastructure/Docker/**: Configuración de Docker

## Estructura de ramas

El repositorio utiliza el siguiente esquema de ramas:

- `master`: Versión estable y producción
- `develop`: Integración de cambios para pruebas
- `backend-api`: Implementación de la API Principal (Persona 1)
- `frontend-ecommerce`: Desarrollo del frontend y API E-Commerce (Persona 2)
- `async-messaging`: Implementación del sistema asíncrono (Persona 3)
- `testing-docs`: Pruebas y documentación (Persona 4)

La implementación actual de la API Principal se encuentra en la rama `backend-api` y debe ser fusionada con `develop` para integración.

## Configuración inicial

### Requisitos previos

- .NET 6 SDK
- Docker Desktop
- Visual Studio Code (recomendado)
- Extensiones de VS Code: C# Dev Kit, Docker

### Pasos para configurar el entorno

1. Clonar el repositorio:
   git clone https://github.com/DerekMarmol/Proyecto-Progra3.git
   cd Proyecto-Progra3

2. Iniciar la base de datos SQL Server con Docker:
    cd SuperBodega.API/Infrastructure/Docker
    docker compose up -d
    cd ../../..

3. Restaurar paquetes NuGet:
    dotnet restore

4. Aplicar migraciones a la base de datos:
    cd SuperBodega.API
    dotnet ef database update

5. Ejecutar la aplicación:
    dotnet run

6. Acceder a Swagger para probar la API:
    http://localhost:5132/swagger/index.html

Credenciales y configuración

    Base de datos SQL Server:

        Server: localhost,1433
        Database: SuperBodegaDB
        User: sa
        Password: YourStrongPassword123!

Configuración en appsettings.json:
    "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=SuperBodegaDB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;"
    }

Flujo de trabajo
    La API implementa las siguientes entidades y operaciones:
    Productos

        GET /api/Productos - Obtener todos los productos
        GET /api/Productos/{id} - Obtener un producto específico
        GET /api/Productos/Proveedor/{proveedorId} - Obtener productos por proveedor
        POST /api/Productos - Crear un nuevo producto
        PUT /api/Productos/{id} - Actualizar un producto
        DELETE /api/Productos/{id} - Eliminar un producto (marcar como inactivo)

    Proveedores

        GET /api/Proveedores - Obtener todos los proveedores
        GET /api/Proveedores/{id} - Obtener un proveedor específico
        GET /api/Proveedores/Activos - Obtener proveedores activos
        POST /api/Proveedores - Crear un nuevo proveedor
        PUT /api/Proveedores/{id} - Actualizar un proveedor
        DELETE /api/Proveedores/{id} - Eliminar un proveedor (marcar como inactivo)

    Clientes

        GET /api/Clientes - Obtener todos los clientes
        GET /api/Clientes/{id} - Obtener un cliente específico
        GET /api/Clientes/Activos - Obtener clientes activos
        POST /api/Clientes - Crear un nuevo cliente
        PUT /api/Clientes/{id} - Actualizar un cliente
        DELETE /api/Clientes/{id} - Eliminar un cliente (marcar como inactivo)

    Compras

        GET /api/Compras - Obtener todas las compras
        GET /api/Compras/{id} - Obtener una compra específica
        GET /api/Compras/Proveedor/{proveedorId} - Obtener compras por proveedor
        GET /api/Compras/Fecha/{fechaInicio}/{fechaFin} - Obtener compras por rango de fechas
        POST /api/Compras - Crear una nueva compra
        PUT /api/Compras/{id} - Actualizar observaciones de una compra

    Ventas

        GET /api/Ventas - Obtener todas las ventas
        GET /api/Ventas/{id} - Obtener una venta específica
        GET /api/Ventas/Cliente/{clienteId} - Obtener ventas por cliente
        GET /api/Ventas/Producto/{productoId} - Obtener ventas por producto
        GET /api/Ventas/Proveedor/{proveedorId} - Obtener ventas por proveedor
        GET /api/Ventas/Fecha/{fechaInicio}/{fechaFin} - Obtener ventas por rango de fechas
        POST /api/Ventas - Crear una nueva venta
        PUT /api/Ventas/CambiarEstado/{id} - Cambiar el estado de una venta

    Lógica de negocio implementada

        Gestión de existencias:

            Al crear una compra, se aumenta automáticamente la existencia de los productos.
            Al crear una venta, se reduce automáticamente la existencia de los productos.
            No se permite vender más unidades de las disponibles en existencia.


        Soft Delete:

            En lugar de eliminar físicamente registros, se marcan como inactivos.
            Hay endpoints específicos para obtener solo los registros activos.


    Estados de ventas:

    Pendiente (0)
    Procesando (1)
    Despachado (2)
    Entregado (3)
    Cancelado (4)

Para desarrolladores
    Flujo de trabajo con Git

    Clonar el repositorio
    Crear una nueva rama para tu tarea:
    git checkout -b nombre-de-tu-rama

    Realizar cambios y confirmarlos:
    git add .
    git commit -m "Descripción de los cambios"

    Subir los cambios a GitHub:
    git push origin nombre-de-tu-rama

    Crear un Pull Request a la rama develop

Integración para la Persona 2 (Frontend y API E-Commerce)

    Debes consumir los endpoints de la API Principal para obtener productos, clientes, etc.
    Implementar la interfaz de usuario que consuma la API.
    Desarrollar la API E-Commerce usando las mismas prácticas de estructura.

Integración para la Persona 3 (Sistemas Asíncronos)

    Implementar el sistema de colas/mensajería (RabbitMQ u otro).
    Crear versiones asíncronas de los endpoints clave.
    Implementar notificaciones por email usando colas.

Integración para la Persona 4 (Testing y Documentación)

    Ampliar la documentación de Swagger con más detalles.
    Implementar pruebas unitarias para repositorios y servicios.
    Crear pruebas de carga usando JMeter o K6.

Ejemplo de flujo completo

    Crear un proveedor (POST /api/Proveedores)
    Crear productos asociados a ese proveedor (POST /api/Productos)
    Crear un cliente (POST /api/Clientes)
    Crear una venta con productos (POST /api/Ventas)
    Verificar que se hayan reducido las existencias (GET /api/Productos/{id})
