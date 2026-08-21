# SmartQueue

SmartQueue es una aplicación web para la **gestión de turnos de un centro de servicios automotrices**. Un empleado inicia sesión, registra solicitudes de atención (cliente + vehículo + servicio), y el sistema genera y administra automáticamente la cola de turnos hasta que cada atención se finaliza y queda guardada en el historial.

Este proyecto corresponde al MVP definido en las historias de usuario HU-00 a HU-04.

---

## 1. Tecnologías utilizadas

- C# y .NET 9
- ASP.NET Core MVC
- Entity Framework Core (enfoque Code First)
- SQL Server
- HTML, CSS y Bootstrap 5
- Git y GitHub

---

## 2. Requisitos

- **.NET 9 SDK**
- **SQL Server** (Express, Developer o LocalDB)
- **Visual Studio 2022** (versión reciente) o Visual Studio Code
- Herramienta de línea de comandos de EF Core (`dotnet-ef`), si desea usar la terminal

Para instalar la herramienta de EF Core (una sola vez):

```
dotnet tool install --global dotnet-ef
```

---

## 3. Cómo abrir el proyecto

1. Descomprima `SmartQueue.zip`.
2. Abra la carpeta `SmartQueue`.
3. En Visual Studio abra el archivo `SmartQueue.csproj` (o la carpeta desde VS Code).
4. Restaure los paquetes NuGet (Visual Studio lo hace automáticamente; en terminal: `dotnet restore`).

---

## 4. Configurar la conexión con SQL Server

Abra `appsettings.json` y ajuste la cadena de conexión `DefaultConnection` a su instancia de SQL Server.

Valor por defecto incluido:

```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SmartQueueDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

Ejemplos según su instalación:

- SQL Server Express: `Server=localhost\\SQLEXPRESS;Database=SmartQueueDb;Trusted_Connection=True;TrustServerCertificate=True;`
- LocalDB: `Server=(localdb)\\MSSQLLocalDB;Database=SmartQueueDb;Trusted_Connection=True;TrustServerCertificate=True;`
- Usuario y contraseña: `Server=localhost;Database=SmartQueueDb;User Id=sa;Password=SU_CONTRASENA;TrustServerCertificate=True;`

Solo debe cambiar la parte de `Server=` (y las credenciales si aplica). El nombre de la base de datos puede dejarse como `SmartQueueDb`.

---

## 5. Crear la base de datos (migraciones)

El proyecto ya incluye la migración inicial. Para crear la base de datos con todas las tablas y el usuario inicial, ejecute:

**Desde la terminal (en la carpeta del proyecto):**

```
dotnet ef database update
```

**O desde Visual Studio** (Package Manager Console):

```
Update-Database
```

Esto crea la base de datos `SmartQueueDb` con las tablas `Usuarios`, `Solicitudes` y `Turnos`, e inserta el usuario inicial.

> Si prefiere regenerar la migración desde cero, borre la carpeta `Migrations` y ejecute:
> `dotnet ef migrations add Inicial` y luego `dotnet ef database update`.

---

## 6. Ejecutar el proyecto

**Desde Visual Studio:** presione F5 o el botón de ejecutar.

**Desde la terminal:**

```
dotnet run
```

Abra el navegador en la dirección que muestre la consola (por ejemplo `https://localhost:7089`).

---

## 7. Credenciales del usuario inicial

El sistema no tiene registro de usuarios. Use el usuario creado por la migración:

- **Correo:** `admin@smartqueue.com`
- **Contraseña:** `Admin123`

La contraseña se guarda en la base de datos como un hash SHA-256, no en texto plano.

---

## 8. Estructura básica del proyecto

```
SmartQueue
├── Controllers      Controladores MVC (Cuenta, Home, Solicitudes, Turnos, Historial)
├── Models           Entidades: Usuario, SolicitudAtencion, Turno + LoginViewModel
├── Data             SmartQueueContext (DbContext de EF Core)
├── Helpers          Seguridad (hash de contraseña)
├── Migrations       Migración inicial de la base de datos
├── Views            Vistas Razor por sección
├── wwwroot          CSS, JS, imágenes y librerías (Bootstrap, jQuery)
├── appsettings.json Cadena de conexión y configuración
├── Program.cs       Configuración de la aplicación
└── SmartQueue.csproj
```

---

## 9. Flujo principal del MVP

1. **Login:** el empleado inicia sesión con el usuario inicial.
2. **Crear solicitud:** registra cliente, vehículo y servicio. Al guardar, el sistema genera automáticamente un turno (`A-001`, `A-002`, ...) con estado *Esperando*.
3. **Cola de turnos:** el empleado pulsa **Llamar siguiente** (el turno pasa a *En servicio*) y luego **Finalizar turno** (pasa a *Finalizado* y sale de la cola).
4. **Historial:** los turnos finalizados quedan disponibles en una vista de solo lectura, con búsqueda por placa o nombre del cliente, ordenados del más reciente al más antiguo.
