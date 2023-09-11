# BackendTestApi
BackendTestApi is a .NET-based API designed to handle Tasks and Projects 
Whether you're building an application to help people sort their tasks based on Priorities(High, Low, Medium) and Statuses (Completed, ) this API will help you also sort the Tasks into Projects. 
This was built on the basis of Enhancing Productivity.
## Features
- **Sorting Tasks and Projects:** Easily create, retrieve, update, and delete tasks and projects.
- **User Management:** Securely manage task data to provide personalized experiences.
- **Notification Integration:** Incorporated with notifications to help, providing users with information about tasks and Projects.


#### PS: This Project is still at it's early stages, more features will be added as time goes on


## Getting Started
These instructions will help you set up the project on your local machine for development and testing purposes.

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- A code editor such as Visual Studio or Visual Studio Code.
  
### Installation
 ```bash
   git clone https://github.com/your-username/BackendTaskAPI.git
 ```

1. Navigate to the project directory:
```bash
 cd BackendTaskAPI
 ```
2. Build the solution
```bash
dotnet build
```
3. Configure the database connection string in ``` appsettings.json. ```
4. Apply the database migrations:
 ```bash
 dotnet ef database update
 ```
5. Run the application:
```bash
dotnet run
```
The API should now be running locally at ``` https://localhost:7203 ```

### Usage
Once the API is up and running, you can use tools like Postman or Swagger to interact with its endpoints. Refer to the API documentation for details on available routes and request formats.

### Documentation
For detailed information on the API endpoints, request and response formats, refer to the API Documentation.

### Contributing
Contributions are welcome! If you find any issues or want to add enhancements, please create a pull request. Make sure to follow the contributing guidelines.

### Acknowledgments
This project was inspired by the love to boost Productivity and also help with other more exciting experiences without hindering Productivity.
Special thanks to the .NET community for their continuous support and contributions.
