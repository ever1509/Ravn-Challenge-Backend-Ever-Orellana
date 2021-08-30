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
    -In-Memory Cache library.
 
 # Environment Deployed in Azure
 
 You can see all of the functionality of the API deployed here https://moviescatalog-api.azurewebsites.net/swagger/
 
 # Setup Locally
 
 #### (First Way) Using dotnet commands: 
 1. Clone the repository
 2. Database Migrations:
    All the migration database are already added to Azure Sql Server using Database migration and the project is pointed properly in the connection but if you want to verify
    you can update or install the database doing the following:
      - Move to the root folder ot the project, and execute the following command:
            - **_dotnet ef database update --project src\Movies.Infrastructure\ --startup-project src\Movies.API_**
 3. To run the API you can move to the project Movies.API with PowerShell with administration privilegies and then execute the following command:
      **_dotnet run_**
 4. Now you can see the API in this URL: https://localhost:5001/swagger/
   
 
 #### (Another way) You can run the API with Docker:
 
 1. After you cloned the repository, also, you can run the API using docker. You can do it in a local environment typing the following commands:	
 
    **_docker build -f .\src\Movies.API\Dockerfile -t moviesapi ._**
    
 2. Then you have run the container built with docker locally with the following command:
 
    **_docker run -it -p 80:80 -d moviesapi_**
    
 3. When all the compilation is completed you can see the docker container in a local way using this URL: http://localhost:80/swagger/
 
    
 
## Users added in the API   ##
**Admin**

    Username: apiadmin@test.com
    Password: Tester@01

**Regular User**

    Username: user1@test.com
    Password: Tester@01
    
 
 Here are screenshots with all the functionalities of the API: <br />
    _First View of the API:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/First.PNG?raw=true)

   _Get all movies catalog without authentication:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/GetAllMovies.PNG?raw=true)
    _Login to the API with using the Admin user:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/AdminLogin.PNG?raw=true)

   _Authenticate in the API with the Admin user token provided:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/AdminAuthenticate.PNG?raw=true)

   _Rate movie with authenticated user:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/RateMovie.PNG?raw=true)

   _Rates by user who only admin has access:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/RatesByUser.PNG?raw=true)

   _Created movie who only admin has access:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/CreatedMovie.PNG?raw=true)

   _Updated movie who only admin has access:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/UpdatedMovie.PNG?raw=true)

   _Deleted movie who only admin has access:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/DeletedMovie.PNG?raw=true)

   _Uploaded image movie who only admin has access:_ <br/>
![alt text](https://github.com/ever1509/Ravn-Challenge-Backend-Ever-Orellana/blob/main/movies-screenshots/UploadedImage.PNG?raw=true)

