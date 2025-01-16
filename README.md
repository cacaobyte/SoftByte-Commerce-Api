# SoftByte Commerce API

Este repositorio contiene la API del sistema **SoftByte Commerce**, un sistema de punto de venta diseñado para el uso interno de la empresa **CacaoByte S.A.**. La API está desarrollada con una arquitectura modular para manejar diferentes capas, incluyendo acceso a datos, lógica de negocios y servicios.

## 🛠️ Tecnologías utilizadas

- **C# / .NET Core**: Framework principal para el desarrollo de la API.
- **PostgreSQL**: Base de datos relacional para almacenar los datos del sistema.
- **Microsoft.Extensions.Configuration**: Configuración para manejar archivos de entorno como `appsettings.json`.
- **Docker**: Para la contenedorización de la aplicación.
- **Git**: Control de versiones.
- **Visual Studio**: Entorno de desarrollo integrado (IDE).

## 📂 Estructura del proyecto

El proyecto sigue una arquitectura por capas para facilitar el mantenimiento y escalabilidad:

1. **`CC` (Capa de Configuración)**:
   Maneja configuraciones comunes, como el acceso a `appsettings.json` y otras configuraciones globales.

2. **`CommerceCore.Api` (API)**:
   Contiene los controladores que exponen los endpoints para las operaciones del sistema.

3. **`CommerceCore.BL` (Lógica de Negocios)**:
   Implementa las reglas de negocio y procesa la lógica de la aplicación.

4. **`CommerceCore.DAL` (Acceso a Datos)**:
   Define los métodos para interactuar con la base de datos PostgreSQL.

5. **`CommerceCore.ML` (Capa de Servicios)**:
   Contiene clases auxiliares para manejo de notificaciones y otros servicios relacionados.

## ⚙️ Configuración inicial

Antes de ejecutar el proyecto, asegúrate de tener los siguientes pasos completados:

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/cacaobyte/SoftByte-Commerce-Api.git
   cd SoftByte-Commerce-Api
