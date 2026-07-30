# 🏢 CEDIVA - Sistema de Gestión de Inventarios y Despachos

**CEDIVA** es un sistema backend diseñado para la gestión eficiente de inventarios, control de despachos y administración de productos, construido con los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**.

Este proyecto está pensado para ser el núcleo de una solución robusta y escalable, permitiendo la independencia de frontend y asegurando las mejores prácticas de desarrollo de software.

---

## 🛠️ Tecnologías y Herramientas

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| **.NET** | 9.0 | Framework principal |
| **C#** | 12 | Lenguaje de programación |
| **Entity Framework Core** | 9.0 | ORM para persistencia de datos |
| **MediatR** | 12.x | Implementación del patrón CQRS y mediador |
| **FluentValidation** | 11.x | Validación de comandos y consultas |
| **SQL Server / PostgreSQL** | - | Base de datos relacional |

---

## 🧱 Arquitectura del Proyecto

El proyecto sigue los principios de **Clean Architecture**, separando las responsabilidades en capas bien definidas:

Cediva.Dominio/ → Entidades, Value Objects, Enums, Interfaces de Repositorios
Cediva.Aplicacion/ → Casos de uso (CQRS: Comandos, Consultas, Handlers)
Cediva.Infraestructura/ → Implementación de DbContext, Repositorios y Servicios externos
Cediva.API/ → Controladores, Middleware y configuración de endpoints

**Principios aplicados:**

- ✅ **Domain-Driven Design (DDD)**
- ✅ **SOLID**
- ✅ **CQRS** (Command Query Responsibility Segregation)
- ✅ **Repository Pattern**
- ✅ **Dependency Injection**

---

## 🚀 Cómo empezar

### Prerrequisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/)

### Paso 1: Clonar el repositorio

```bash
git clone https://github.com/bohorquezc734-commits/CedivaBackend.git
cd CedivaBackend 

Restaurar paquetes

dotnet restore


Paso 3: Configurar la base de datos
Actualiza la cadena de conexión en Cediva.API/appsettings.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CedivaDB;User Id=sa;Password=tu_contraseña;TrustServerCertificate=True;"
  }
}
Ejecuta las migraciones:

bash
cd Cediva.API
dotnet ef database update

Paso 4: Ejecutar la aplicación
bash
dotnet run --project Cediva.API
La API estará disponible en: https://localhost:5001/api

La documentación Swagger estará en: https://localhost:5001/swagger


Estructura de Carpetas del Proyecto
text
CedivaBackend/
│
├── Cediva.Dominio/                 # Capa Core (entidades, interfaces)
├── Cediva.Aplicacion/              # Casos de uso (CQRS)
├── Cediva.Infraestructura/         # Persistencia y servicios externos
├── Cediva.API/                     # Controladores y puntos de entrada
├── Cediva.UnitTests/               # Pruebas unitarias
├── Cediva.IntegrationTests/        # Pruebas de integración
├── Cediva.sln                      # Solución de Visual Studio
├── .gitignore                      # Archivos ignorados por Git
└── README.md                       # Este archivo

Módulos Principales
Módulo	Descripción
Productos	Administración de productos, categorías y marcas.
Inventario	Control de stock, movimientos y lotes.
Despachos	Gestión de órdenes de despacho, guías y seguimiento.
Escaneo	Endpoints para lectura de códigos de barras.


Estado del Proyecto
Capa	Estado
✅ Dominio	Completamente implementado
🟡 Aplicación	Estructura definida, en desarrollo
❌ Infraestructura	Pendiente de implementación
❌ API	Pendiente de implementación

Contribuciones
Si deseas contribuir al proyecto:

Haz un Fork del repositorio

Crea una rama con tu funcionalidad: git checkout -b feature/nueva-funcionalidad

Commit de tus cambios: git commit -m 'Añadir nueva funcionalidad'

Push a la rama: git push origin feature/nueva-funcionalidad

Abre un Pull Request

Contacto
Autor: Carlos Arturo Bohorquez Cabrera

Email: bohorquezc734@gmail.com

GitHub: bohorquezc734-commits


