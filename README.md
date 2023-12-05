# Prototype-v1

Línea base para construccuión de apis basadas en .net 5, bajo los enfoques propuestos de data driven, clean architecture y el uso de los proncipios de desarrollo SOLID.

# Pila Tecnologíca
- .net 5
- Dapper
- FluentValidation
- Mediator
- Swagger
- JWT Bearer token `autentication`

# Instruccion de Uso
- Descargar el backup de la base de datos que se encuentra en la ruta "Data"
- Actaulizar los settings de conectión string:
- "ConnectionStrings": {
    "PrototypeConnection": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
  }
 - El aplicativo guarda las imagenes en file storage por tal motivo se debe actaulizar el setting de la ruta:
 -  "FileSettings": {
    "pathOwnerPhotos": "c://proptotype/"
  },
  
- Existe un API de autenticación la cual es la encargada de devolver el token necesario para el consumo de las demás APIs.  
