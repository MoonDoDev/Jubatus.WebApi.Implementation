# Jubatus.WebApi.Implementation - Implementación de una WebApi para el manejo de Usuarios.

## Descripción General

Este es un proyecto de ejemplo que implementa una WebApi en .NET 8, el cual permite ejecutar un CRUD de Usuarios sobre una colección de [**MongoDB**](https://www.mongodb.com), y para su construcción nos estamos apoyando del Nuget Package [**Jubatus.WebApi.Extensions**](https://www.nuget.org/packages/Jubatus.WebApi.Extensions/).

```
  Jubatus.WebApi.Implementation \
  | api.gateway.users \             // Proyecto WebApi que implementa ReverseProxy como Gateway.
    | api.gateway.users.csproj      // Archivo para el manejo del proyecto.
    | api.gateway.users.http        // Archivo http para probar los end-points.
    | appsettings.Development.json  // Archivo de configuración para Desarrollo.
    | appsettings.json              // Archivo de configuración para Producción.
    | Constants.cs                  // Definición de constantes para el proyecto.
    | Program.cs                    // Módulo principal con la definición de la WebApi.
  | api.service.users \             // Proyecto WebApi que implementa el CRUD de Usuarios.
    | Controllers \
      | AuthController.cs           // Controller con el end-point para la autenticación JWT.
      | UsersController.cs          // Controller con los end-points para el CRUD.
    | Dtos \
      | UsersDto.cs                 // Definición de DTOs para manejar datos de Usuario.
    | Models \
      | TokensModel.cs              // Modelo para el manejo de los Tokens.
      | UsersEntity.cs              // Modelo para el manejo de los datos de Usuario.
    | api.service.users.csproj      // Archivo para el manejo del proyecto.
    | api.service.users.http        // Arcvhivo http para probar los end-points.
    | appsettings.Development.json  // Archivo de configuración para Desarrollo.
    | appsettings.json              // Archivo de configuración para Producción.
    | Constants.cs                  // Definición de constantes para el proyecto.
    | Exceptions.cs                 // Definición de excepciones para el proyecto.
    | Extensions.cs                 // Definición de extensiones para Usuarios.
    | Program.cs                    // Módulo principal con la definición de la WebApi.
  | allprojects.sln                 // Archivo para manejar todos los proyectos de la solución.
  | compose.yaml                    // Archivo para el despliegue del proyecto con Docker.
  | dockerfile.gateway              // Archivo Docker para construir la imagen de Api Gateway.
  | dockerfile.service              // Archivo Docker para construir la imagen de Api Usuarios.
  | gateway.sln                     // Archivo para compilar el proyecto de Api Gateway.
  | LICENSE                         // Licencia tipo MIT para el uso del proyecto.
  | README.Docker.md                // Documentación útil para el uso de Docker.
  | service.sln                     // Archivo para compilar el proyecto de Api Usuarios.    
```

### Entre las características más importantes, tenemos las siguientes:

- [x]  Se implementa un WebApi con las características de un ReverseProxy para cumplir funciones de Api Gateway y enrutar los Request hacia el Api de Usuarios.
- [x]  El Api Gateway cuenta con un end-point HealthCheck para validar su disponibilidad (Salud).
- [x]  Se implementa un WebApi con los 5 end-points requeridos para ejecutar el CRUD de los Usuarios.
- [x]  El WebApi de Usuarios cuenta con dos end-points adicionales, que permiten validar su estado de salud (HealthChecks), del Servicio y del repositorio, que en este caso es MongoDB.
- [x]  Los end-points retornan los datos de Usuario con los [**Status Codes de HTTP**](https://www.restapitutorial.com/httpstatuscodes) correspondientes.
- [x]  El end-point para consultar todos los registros de Usuario en la BD, retorna un [**IAsyncEnumerable**](https://www.milanjovanovic.tech/blog/csharp-yield-return-statement#working-with-iasyncenumerable), lo cual nos permitirá iterar la colección de respuesta asincrónicamente.
- [x]  El WebApi de Usuarios, requiere que quien lo vaya a consumir se autentique previamente para asignarle un [**Bearer Token - JWT**](https://jwt.io/introduction).
- [x]  Los end-points para ejecutar el CRUD de Usuarios, requieren de una autorizacion a través de un Bearer Tokens - JWT, que solo será asignado al usuario autenticado.
- [x]  El Bearer Token asignado al Usuario autenticado tendrá una vigencia máxima de 10 minutos.
- [x]  En los controladores y los end-points del WebApi de Usuarios, se implementa el [**manejo de versiones**](https://weblogs.asp.net/ricardoperes/asp-net-core-api-versioning), para facilitar una transición sin traumas hacia las futuras versiones de la WebApi.
- [x]  En el WebApi de Usuarios se está implementando [**RateLimiter**](https://learn.microsoft.com/en-us/dotnet/api/system.threading.ratelimiting.ratelimiter?view=aspnetcore-8.0) tipo "*fixed*", para evitar la sobrecarga y el abuso en el consumo del Servicio.
- [x]  En los DTOs y Modelos se implementa [**DataValidation**](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs) para minimizar los riesgos de inconsistencias en los datos ingresados por el consumidor.
- [x]  Igualmente en los DTOs y los Modelos se está utilizando el atributo `[NotLogged]` para evitar exponer información sensible cuando se utilicen herramientas de "*Logging*" como el [**SeriLog**](https://serilog.net/).
- [x]  Para el manejo de las configuraciones del proyecto, se están utilizando directamente las clases expuestas por el paquete `Jubatus.WebApi.Extensions`. y localmente al proyecto, se están trabajando con [**user-secrets**](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux) para Desarrollo y con variables de entorno a través de Contenedores de Docker para Producción.
- [x]  Para la persistencia de los datos se está utilizando un repositorio de MongoDB, configurado a través de las extensiones expuestas por el paquete `Jubatus.WebApi.Extensions`.
- [x]  Para facilitar el despliegue de la funcionallidad en cualquiera de los proveedores de Nube, las Apis se contenerizan con Docker.
- [x]  La contenerización de la solución, permite que la funcionalidad se pueda escalar eventualmente de forma horizontal, de manera que se pueda alterar el número de las instancias del contenedor, según como lo requiera la demanda del momento. (esto requiere de componentes de orquestación como el Kubernetes).
- [x] La solución está diseñada para que se creen dos contenedores, uno el que corresponde al Api Gateway y el segundo, correspondiente al Api de Usuarios. Esto se logra ejecutando el siguiente comando:

```
docker build -f dockerfile.gateway -t users.gateway:latest .
docker build -f dockerfile.service -t users.service:latest .
```

> [!IMPORTANT]
> - [x]  El archivo `dockerfile.gateway` requiere del archivo `gateway.sln` para compilar y generar la imagen del Api Gateway.
> - [x]  El archivo `dockerfile.service` requiere del archivo `service.sln` para compilar y generar la imagen del Api de Usuarios. 

- [x]  Para crear las instancias de los contenedores y realizar el despliegue de la funcionalidad con Docker, debemos ejecutar el siguiente comando:

```
docker compose up -d
```

> [!IMPORTANT]
> Al crear el despliegue, ejecutando el comando anterior, se descarga (en caso de no tenerla) una imagen Docker de MongoDB, para poder crear y configurar localmente un repositorio de dicha BD.

- [x]  Para eliminar las instancias de los contenedores, debemos ejecutar el siguiente comando:

```
docker compose down
```

## Menciones y agradecimientos:

- [x]  Agradezco el gran apoyo y sus valiosas recomendaciones de [**Eddie Velasquez**](https://github.com/eddievelasquez), mi mentor y amigo.
- [x]  Gracias a / Thanks so much to [**Timo Vilppu**](https://github.com/vilppu) por su valioso aporte y la información suministrada sobre el tema `IAsyncEnumerable`.
- [x]  Gracias tambien a / Thanks so much to [**Michael Altmann**](https://github.com/altmann) por su aporte e información sobre `FluentResults`.
- [x]  Finalmente y no menos importante, un agradecimiento por el valioso aporte que hacen a la comunidad [**Stefan Djokic**](https://thecodeman.net/) y [**Milan Jovanović**](https://www.milanjovanovic.tech/)

## Dependencias

```
"Microsoft.Extensions.Configuration" Version="8.0.0"
"Microsoft.Extensions.Configuration.UserSecrets" Version="8.0.0"
"Yarp.ReverseProxy" Version="2.1.0"
"Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.8"
"Microsoft.AspNetCore.OpenApi" Version="8.0.4"
"Swashbuckle.AspNetCore" Version="6.7.0"
"Destructurama.Attributed" Version="4.0.0"
"Jubatus.WebApi.Extensions" Version="1.2.31"
"SonarAnalyzer.CSharp" Version="9.32.0.97167"
```

---------

[**YouTube**](https://www.youtube.com/@hectorgomez-backend-dev/featured) -- 
[**LinkedIn**](https://www.linkedin.com/in/hectorgomez-backend-dev/) -- 
[**GitHub**](https://github.com/MoonDoDev/Jubatus.WebApi.Implementation)
