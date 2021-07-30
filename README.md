# Prototype-v1

Proyecto asociado a la prueba tecnica propuesta para el cargo para desarrollador Senior, basandose en en los enfoques propuestos de data driven, clean architecture y el uso de los proncipios de desarrollo SOLID.

# Pila Tecnologíca
- .net 5
- Dapper
- FluentValidation
- Mediator
- Swagger
- JWT Bearer token autentication

# Instruccion de Uso
- Descargar el backup de la base de datos que se encuentra en la ruta "Data"
- actaulizar los settings de conectión string:
  "ConnectionStrings": {
    "PrototypeConnection": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
  }
 - El aplicativo guarda las imagenes en gile storage por tal motivo se debe actaulizar el setting de la ruta:
   "FileSettings": {
    "pathOwnerPhotos": "c://proptotype/"
  },
  
- Existe un api de autenticación la ula devuelve la información de inicio de sessión en especial el token necesario para el consumo de las demás APIs.  
