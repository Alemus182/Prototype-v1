# Prototype-v1

Baseline for construction of an API based on .net 7, under the proposed clean architecture approaches and the use of SOLID development principles.
As well as a single page application base in angular like a front end.

# Technology Stack
## Backend
    - .net 7
    - Minimal Apis
    - FluentValidation
    - Mediator
    - NLog
    - JWT Bearer token `autentication`
## Frontend
    - Angular 10
    - Angular Material
    - Boostrap
    - RxJs
# Assumptions
- The application uses InMemoryCache and it is refreshed every hour
- Authentication is based on username and password and it generates a JWT.
  The only validation that is carried out is that it is a valid email and
  that the password has more than 8 characters.
# Execution

# Demo
An implementation was carried out which is available at:
- [Web Site](https://wonderful-wave-0f1681e1e.4.azurestaticapps.net/)
- [Web Api](https://nextech-demo-api.azurewebsites.net/)







