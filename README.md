# 🔐 Módulo de Seguridad Múltiple

> Sistema de autenticación y autorización flexible para aplicaciones empresariales. Soporta múltiples proveedores de identidad, roles jerárquicos y políticas de acceso granulares. Desarrollado con ASP.NET Core, SQL Server y buenas prácticas de seguridad.

![Estado](https://img.shields.io/badge/estado-finalizado-green)
![Licencia](https://img.shields.io/badge/licencia-MIT-green)
![C#](https://img.shields.io/badge/C%23-9.0-239120?logo=csharp)
![ASP.NET](https://img.shields.io/badge/ASP.NET%20Core-6.0-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2014+-CC2927?logo=microsoftsqlserver)

---

## 🎯 Descripción del Proyecto

Este módulo proporciona una capa de seguridad reutilizable para aplicaciones .NET, permitiendo gestionar:

- ✅ Autorización basada en roles (RBAC) y permisos granulares
- ✅ Gestión de usuarios, grupos y políticas de acceso
- ✅ Auditoría de eventos de seguridad (logins, cambios de permisos, etc.)
- ✅ Configuración flexible mediante archivos JSON o base de datos

### Problema que resuelve
- Unificar mecanismos de autenticación dispersos en sistemas legacy
- Centralizar la gestión de permisos para facilitar auditorías
- Reducir la complejidad al integrar nuevos proveedores de identidad
- Garantizar cumplimiento de políticas de seguridad institucional

### Valor para el negocio
- 🔒 Mayor control y trazabilidad sobre accesos al sistema
- ⚡ Reducción de tiempo en onboarding de nuevos usuarios/roles
- 🔄 Facilidad para migrar entre proveedores de autenticación
- 📊 Reportes de seguridad para cumplimiento normativo

---

## 🏗️ Arquitectura del Sistema

```mermaid
graph TD
    User[Usuario] -->Auth{Proveedor de Autenticación}    
    Auth -->|Local| LocalDB[(SQL Server - Users)]       
    
    Auth --> Policy[Validación de Políticas]
    Policy --> Resource[Recurso Protegido]
    
    Auth --> Audit[Registro de Auditoría]
    
    style Auth fill:#239120,color:white
    style Policy fill:#512BD4,color:white
