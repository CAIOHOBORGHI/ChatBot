# ChatBot Web Application
A chat bot web application focused on the high quality <b>back-end</b> code.

### Features
- :lock: &nbsp;Authentication with IdentityServer 
- :speech_balloon: &nbsp;Chat Room for logged users
- :chart_with_upwards_trend: &nbsp;Command **/stock=*code*** gets price of stock from Stooq
- :x: &nbsp;Command errors are handled by chat Administrator
- :stopwatch: &nbsp;Messages are ordered by time (older ones are shown in top)
- :boom: &nbsp;Only 50 messages are displayed in the chat ( No need to reload the page, js makes the magic in real-time )

### Built with
* [Docker :whale:](https://docker.com) 
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)
* [IdentityServer](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore)
* [.NET 5](https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
* [RabbitMQ](https://www.rabbitmq.com/)

## Getting Started

### Prerequisites
- Docker
- Docker Compose

or

- .NET 5 Build Sdk && .NET 5 Runtime
- Microsoft SQL Server 2017
- RabbitMQ

## Usage
### With docker
1. In the root folder, run 
  ```
  docker-compose build
  ```
2. Then you can set it all up with
  ```
  docker-compose up -d
  ```
  - Notes 
    - Web and Worker containers will be restarted until RabbitMQ server container is ready for connections</i>
    - If you´re running on Windows, you need to set COMPOSE_CONVERT_WINDOWS_PATHS=1
    - If you need to scale the worker container, just remove the **container-name** on *docker-compose.yml* and run
      ```
      docker-compose up --scale worker=3
      ```

3. Open your browser in http://localhost:4000 
4. Create your account
5. Start chatting
    

### Without docker
1. Start your RabbitMQ Server + MSSQL Server 2017
2. Update the appSettings in Chat.Web/appsettings.json and Chat.Worker/appsettings.json
3. In the root folder, run
  ```
  dotnet restore
  dotnet build
  ```
4. Then, start worker with
  ```
  cd Chat.Worker
  dotnet run
  ```

5. In another terminal, run the following command in the root folder, to start web application  
  ```
  cd Chat.Web
  dotnet run
  ```

6. Open your browser in http://localhost:5000 
7. Create your account
8. Start chatting

### Tests
On the project root, run this command to run all tests
  ```
  cd Chat.Tests
  dotnet test
  ```
 
  
## Contact
[![LinkedIn][linkedin-shield]][linkedin-url] 

[linkedin-shield]: https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white
[linkedin-url]: https://www.linkedin.com/in/caio-borghi-0334b1160/
