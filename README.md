# Ravn-Challenge-Backend-Ever-Orellana
This project is an API to manage Movies Catalog.

# Solution
  
  ## Architecture and Design Implemented:
    -Mediator
    -Clean Architecture
    -Dependency Injection
    
  ## Frameworks, Package and Tools:
    -.Net Core 3.1
    -EF Core
    -MediatR
    -Automapper
    -Fluent API
    -XUnit
    -Swagger
    -Identity
    -FluentValidation
    -Azure Blob Storage to upload images
    -Azure Sql Server
    -Azure in the deployment.
    -FLuentAssertions in the Integration Tests
    -Ef Migrations.
    
 # Setup
 1. Clone the repository
 2. Database Migrations:
    All the migration database are already added to Azure Sql Server using Database migration and the project is pointed properly in the connection but if you want to verify
    you can update or install the database doing the following:
      - Move to the root folder ot the project, and execute the following command:
            - **_dotnet ef database update --project src\Movies.Infrastructure\ --startup-project src\Movies.API_**
 3. To run the API you can move to the project Movies.API with PowerShell with administration privilegies and then execute the following command:
      1) **_dotnet run_**
    
 You can see all of the functionality of the API deployed here https://moviescatalog-api.azurewebsites.net/swagger/
 
## Users added in the API   ##
**Admin**

    Username: apiadmin@test.com
    Password: Tester@01

**Regular User**

    Username: user1@test.com
    Password: Tester@01
    
 
 Here are screenshots with all the functionalities of the API:
    _First View of the API:_
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/master/movies-screenshots/First.PNG?raw=true)
