# API Cartelera de Películas (.NET 10)

[![Restaurar dependencias](https://img.shields.io/badge/1%20-Restaurar%20dependencias-512BD4?logo=dotnet&logoColor=white)](#1-restaurar-dependencias)
[![Configurar base de datos](https://img.shields.io/badge/2%20-Configurar%20base%20de%20datos-0078D4?logo=microsoftsqlserver&logoColor=white)](#2-configurar-la-base-de-datos)
[![Crear base de datos](https://img.shields.io/badge/3%20-Crear%20base%20de%20datos-107C10?logo=databricks&logoColor=white)](#3-crear-la-base-de-datos)
[![Cargar datos de prueba](https://img.shields.io/badge/4%20-Cargar%20datos%20de%20prueba-F59E0B?logo=microsoftsqlserver&logoColor=white)](#4-cargar-datos-de-prueba-opcional)
[![Ejecutar API](https://img.shields.io/badge/5%20-Ejecutar%20API-2EA44F?logo=swagger&logoColor=white)](#5-ejecutar-la-api)

## Puesta en marcha

### 1. Restaurar dependencias

```powershell
dotnet restore
```

### 2. Configurar la base de datos

1. Copia `appsettings.Development.json` en la raíz del proyecto si aún no existe.
2. Ingresa la URL o cadena de conexión de tu instancia de SQL Server en `ConnectionStrings:DefaultConnection`.
3. La configuración local inicial es:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PeliculasTaller;User Id=sa;Password=<tu_password>;TrustServerCertificate=True;Encrypt=True;"
  }
}
```

Reemplaza `<tu_password>` por la contraseña de tu usuario de SQL Server. `appsettings.Development.json` es un archivo local y está excluido del repositorio para no publicar credenciales.

### 3. Crear la base de datos

Ejecuta cada comando en la terminal:

```powershell
dotnet tool install --global dotnet-ef # Solo si aún no tienes la herramienta
dotnet ef migrations add ModeloInicial
dotnet ef database update
```

### 4. Cargar datos de prueba (opcional)

Ejecuta `Scripts/datos_prueba.sql` en tu cliente de SQL Server. Para hacerlo desde `sqlcmd`:

```powershell
sqlcmd -S localhost -d PeliculasTaller -U sa -P "<tu_password>" -i .\Scripts\datos_prueba.sql
```

### 5. Ejecutar la API

```powershell
dotnet watch run --launch-profile https
```

Abre Swagger en [https://localhost:7256/swagger](https://localhost:7256/swagger).

Las imágenes subidas se guardan en `wwwroot/<contenedor>/` y se sirven en `https://localhost:7256/<contenedor>/<archivo>`.

## Botones básicos

Estos enlaces permiten acceder rápidamente a las acciones habituales del repositorio:

[![Código](https://img.shields.io/badge/Ver%20código-181717?logo=github&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API)
[![Ramas](https://img.shields.io/badge/Ver%20ramas-1f883d?logo=git&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API/branches)
[![Issues](https://img.shields.io/badge/Reportar%20issue-d73a4a?logo=github&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API/issues/new/choose)
[![Clonar](https://img.shields.io/badge/Clonar%20repositorio-0969da?logo=git&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API)

## Contribuir desde una rama aparte

Para que los cambios puedan revisarse antes de integrarlos a `main`, sigue este flujo:

```powershell
git clone https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API.git
cd PeliculasOnline-TdeA-API
git switch main
git pull origin main
git switch -c feature/<descripcion-del-cambio>
```

Realiza los cambios, prueba la aplicación y luego publícalos en tu rama:

```powershell
git add .
git commit -m "Describe brevemente el cambio"
git push -u origin feature/<descripcion-del-cambio>
```

No subas `appsettings.Development.json`, contraseñas ni otros secretos. Abre un Pull Request desde tu rama hacia `main`, explica los cambios y espera la revisión antes de solicitar la integración.

## Botones de integración

[![Abrir Pull Request](https://img.shields.io/badge/Abrir%20Pull%20Request-238636?logo=github&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API/compare)
[![Comparar cambios](https://img.shields.io/badge/Comparar%20cambios-8250df?logo=github&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API/compare/main...)
[![Pull Requests](https://img.shields.io/badge/Revisar%20Pull%20Requests-0969da?logo=github&logoColor=white)](https://github.com/JuanDavidChalarcaMarin/PeliculasOnline-TdeA-API/pulls)

En GitHub: selecciona **Compare & pull request**, confirma que la rama base sea `main` y que la rama de comparación sea tu rama `feature/...`. La integración a `main` debe realizarse únicamente después de verificar el código y aprobar el Pull Request.
