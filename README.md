## Car Workshop System

A simple backend system for managing car workshop orders built with ASP.NET Core Web API.
The project was created as a learning and portfolio project to demonstrate the implementation of several important backend architecture and design patterns.

The application allows a workshop owner to manage repair orders and assign mechanics, while mechanics can update the progress of their work.

## Overview

The system models a basic workflow of a car repair workshop:
- The Workshop Owner manages repair orders.
- The Mechanics work on assigned orders and update their progress.


## Technologies

- ASP.NET Core Web API
- ASP.NET Core Identity
- JWT Authentication
- FluentValidation
- xUnit (basic tests)

## Architecture

The project follows the principles of Clean Architecture, separating the application into independent layers with clear responsibilities.
Typical layers include:
- Domain
- Application
- Infrastructure
- API

## Design Patterns Used

- Repository Pattern
- Result Pattern
- Clean Architecture

## Authentication & Authorization

The application uses:
- ASP.NET Core Identity
- JWT (JSON Web Token) Authentication

Role-based access control is used to separate permissions between:
- Workshop Owner
- Mechanic

## Possible Future Improvements
Some possible extensions to the system could include:

- full integration testing
- better test coverage
- logging and monitoring
- frontend client (e.g. React or Angular)
- notification system for order updates
