# Micro-servicio con Api REST para manejo de Usuarios

## Descripción General

Esta es una Solución desarrollada con C# de .NET 8, MongoDB, y Docker, y con la cual podemos gestionar el Manejo de Usuarios a través de un micro-servicio con una REST API.

### Entre las características de la solución, tenemos las siguientes:

- [x]  Incluye un micro-servicio con una REST API (Proyecto tipo WebApi)

- [x]  La REST API expone 5 end-points, con los cuales se podrá Consultar / Crear / Actualizar / Eliminar la información de los Usuarios en la BD.

- [x]  Para la persistencia de la información de los Usuarios, se utiliza una BD no relacional, tipo MongoDB, con una colección (Usuarios).

- [x]  Para efectos de asegurar el acceso y el consumo de la REST API, se implementó la Autenticación con Bearer Token (JWT), por lo tanto, solo los usuarios autorizados podrán consumir el micro-servicio.

- [x]  Para evitar la sobrecarga y el abuso en el consumo del Servicio, se implementó un Rate-Limiter, caraterística que nos permite limitar la cantidad de llamados de un mismo usuario, entre otras bondades.

- [x]  Para garantizar una transición sin traumas hacia las futuras versiones de la REST API, se implementó Api-Versioning.

- [x]  La REST API cuenta con dos end-points adicionales, que permiten validar su estado de salud (HealthChecks), incluyendo la BD (MongoDB).

- [x]  Igualmente se implementó un Reverse-Proxy a modo de Api Gateway, para atender en primer instancia las solicitudes de los usuarios, y posteriormente enrutarlas al micro de usuarios, y asi garantizar, entre otras cosas, que si se cambia la ubicación del servicio que gestiona los usuarios, el consumidor no se vea afectado.

- [x]  Para poder desplegar la funcionallidad en cualquiera de los proveedores de Nube, se conteneriza con Docker.

- [x]  La contenerización de la solución, permite que la funcionalidad se pueda escalar eventualmente de forma horizontal, de manera que se pueda alterar el número de las instancias del contenedor, según como lo requiera la demanda del momento. (esto requiere de componentes como el Kubernetes).

---------

[**YouTube**](https://www.youtube.com/@hectorgomez-backend-dev/featured) - 
[**LinkedIn**](https://www.linkedin.com/in/hectorgomez-backend-dev/)
