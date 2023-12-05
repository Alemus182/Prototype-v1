# Prototype-v1

Baseline for construction of an API based on .net 5, under the proposed clean architecture approaches and the use of SOLID development principles.
As well as a single page application base in angular 

# Technology Stack
## Backend
    - .net 5
    - Minimal Apis
    - FluentValidation
    - Mediator
    - JWT Bearer token `autentication`
## Frontend
    - Angular 10
    - Angular Material
    - Boostrap
    - RxJs
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
