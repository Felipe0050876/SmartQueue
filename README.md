# SmartQueue

SmartQueue es un sistema web desarrollado para facilitar la organización y gestión de turnos en centros de servicios automotrices. El proyecto surge como una solución para mejorar el control del orden de atención de los vehículos y mantener en un mismo sistema la información relacionada con los clientes, sus vehículos y los servicios solicitados.

## Descripción del proyecto

El sistema permite registrar solicitudes de atención con los datos principales del cliente, vehículo y servicio requerido. A partir de cada solicitud, SmartQueue genera automáticamente un turno con un código único y consecutivo, permitiendo al personal del establecimiento visualizar y gestionar la cola de atención.

Los turnos pasan por los estados **Esperando**, **En servicio** y **Finalizado**, permitiendo al empleado llamar al siguiente vehículo y finalizar su atención desde el sistema. Una vez completado el servicio, el registro permanece disponible en un historial para futuras consultas.

## Funcionalidades principales

- Inicio de sesión para empleados.
- Registro y gestión de solicitudes de atención.
- Registro de los datos del cliente, vehículo y servicio solicitado.
- Generación automática de códigos de turno.
- Visualización de la cola de turnos activos.
- Gestión de los estados de cada turno.
- Función para llamar al siguiente turno.
- Finalización de turnos atendidos.
- Registro de fecha y hora de inicio y finalización de la atención.
- Historial de turnos finalizados.
- Búsqueda en el historial por nombre del cliente o placa del vehículo.

## Tecnologías utilizadas

El proyecto fue desarrollado utilizando:

- C#
- .NET 9
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- HTML
- CSS
- Bootstrap
- Git y GitHub

## Metodología de trabajo

SmartQueue fue desarrollado como parte de un Sprint de una semana utilizando la metodología ágil **Scrum**. El trabajo fue organizado mediante historias de usuario y tareas, distribuidas entre los integrantes del equipo según las funcionalidades establecidas para el MVP.

Para el control de versiones se utilizó Git y GitHub, trabajando con ramas independientes para las diferentes funcionalidades y realizando posteriormente su integración al proyecto.

## Objetivo del producto

El objetivo de SmartQueue es proporcionar a los centros de servicios automotrices una herramienta sencilla que permita organizar y gestionar de manera eficiente los turnos de atención, facilitando el seguimiento de los vehículos que se encuentran en espera, en servicio o que ya fueron atendidos.

## Alcance

Esta versión corresponde al **MVP (Producto Mínimo Viable)** de SmartQueue. Por esta razón, el proyecto se concentra únicamente en las funcionalidades esenciales necesarias para demostrar el funcionamiento de la solución y validar su propósito principal.
