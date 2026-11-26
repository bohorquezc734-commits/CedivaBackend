# ⚙️ CEDIVA - Backend Logístico (API REST)

Este es el núcleo backend de **CEDIVA**, un sistema empresarial de gestión logística construido con los más altos estándares de la industria. Permite la administración de inventarios, verificación de picking & packing en muelle (auditoría), despachos y control de usuarios con seguridad basada en roles (RBAC).

El proyecto está diseñado bajo los principios de **Clean Architecture**, **Domain-Driven Design (DDD)** y **CQRS**, garantizando escalabilidad, mantenibilidad y un dominio de negocio robusto.

## 🛠️ Stack Tecnológico
- **.NET 9** / **C# 12**
- **Entity Framework Core 9** (SQL Server)
- **MediatR** (Patrón CQRS)
- **FluentValidation** (Validación de comandos)
- **JWT** (Autenticación y Autorización)
- **ProblemDetails** (Manejo estandarizado de errores)

---

## 🏗️ Arquitectura del Proyecto

El backend está dividido en 4 capas desacopladas:

- **`Cediva.Domain`**: Núcleo del negocio. Contiene Agregados Raíz (`Producto`, `Inventario`, `OrdenFacturaMuelle`, `Despacho`), Value Objects (`Cantidad`, `CodigoBarras`, `Direccion`), Enums e Interfaces de repositorios. Las reglas de negocio están encapsuladas aquí.
- **`Cediva.Application`**: Casos de uso mediante CQRS. Comandos (mutaciones) y Queries (lecturas) con MediatR, validaciones con FluentValidation y DTOs.
- **`Cediva.Infrastructure`**: Implementación de persistencia (EF Core 9, Fluent API, Migraciones) y repositorios concretos.
- **`Cediva.API`**: Puntos de entrada HTTP. Controladores REST, Middlewares globales de excepciones y configuración de CORS/JWT.

---

## ⚙️ Flujos Principales del Sistema

### 1. Verificación en Muelle, Picking & Packing (Auditoría)
El flujo de despacho más crítico se maneja a través del agregado `OrdenFacturaMuelle`.
- Una orden nace en estado `"pendiente"`.
- Los operarios (Auditores) pistoleán los códigos de barras.
- Al confirmar, el sistema valida cantidades y cambia el estado a `"auditada_lista_despacho"` (guardando quién y a qué hora auditó).
- Finalmente, se asigna el transporte (transportadora, placa, conductor) y pasa a `"despachada"`.

### 2. Gestión de Inventario
- Registro de entradas/salidas de productos.
- Uso de Value Objects para garantizar que las cantidades nunca sean negativas.
- Asociación de productos a categorías y marcas.

### 3. Seguridad y Roles (RBAC)
La API implementa autenticación JWT. Los endpoints están protegidos según el rol del usuario:
- **Administrador**: Acceso total.
- **Supervisor**: Gestión comercial y auditoría.
- **AuditorBodega**: Acceso restringido a la operación de muelle.

---

## 📡 Endpoints Principales (Muelle)

| Método | Endpoint | Roles Permitidos | Descripción |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/MuelleControlador` | Admin, Supervisor, Auditor | Lista órdenes con sus líneas. |
| `POST` | `/api/MuelleControlador` | Admin, Supervisor | Crea factura en estado `"pendiente"`. |
| `PUT` | `/api/MuelleControlador/{id}/auditar` | Admin, Supervisor, Auditor | Cierra auditoría y cambia a `"auditada_lista_despacho"`. |
| `PUT` | `/api/MuelleControlador/{id}/despachar` | Admin, Supervisor, Auditor | Registra transporte y cambia a `"despachada"`. |

---

## 🚀 Cómo ejecutar el proyecto localmente

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/bohorquezc734-commits/CedivaBackend.git
