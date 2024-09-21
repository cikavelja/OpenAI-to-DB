# Sample Project with UnitOfWork, OpenTelemetry, and OpenAI Integration

### Overview
This repository contains a sample project demonstrating the use of the Unit of Work pattern, OpenTelemetry for distributed tracing, and integration with the OpenAI API. The project also includes CRUD operations with database support and mashup functionalities.

### Master Branch
- **UnitOfWork Pattern**: Implements the UnitOfWork pattern to manage database transactions efficiently. This pattern helps in maintaining a list of operations that need to be executed within a transactional scope.
- **OpenTelemetry**: Integrates OpenTelemetry to provide distributed tracing and monitoring capabilities. This helps in tracking the application's performance and tracing requests through the system.
- **OpenAI API Integration**: Connects to the OpenAI API to utilize its language models for various text processing tasks.
- **CRUD Operations**: Includes basic Create, Read, Update, and Delete operations for interacting with a database.
- **Mashup Functionalities**: Combines OpenAI's capabilities with CRUD operations, allowing for enhanced data processing and manipulation.
- **Voice Recognition for Database Commands**: Integrates voice recognition using the Web Speech API, allowing users to perform database operations such as Create, Read, Update, and Delete via voice commands..

## Features
* Full CRUD implementation with a database of your choice (e.g., SQL Server, SQLite).
* Integration with the OpenAI API for text generation and processing.
* Example mashup of CRUD operations with OpenAI responses.

## Features
* Full CRUD implementation with a database of your choice (e.g., SQL Server, SQLite).
* Integration with the OpenAI API for text generation and processing.
* Example mashup of CRUD operations with OpenAI responses.
* Voice Recognition Commands: Allows users to interact with the API via voice commands, translating spoken language into executable database actions.
   * Voice-activated Database Operations: Speak commands like "Create new record," "Read all records," "Update record with ID 1," or "Delete record with ID 2," and the system will process the voice input, generate the corresponding SQL       query using OpenAI, and execute the operation in the database.
   * Real-time Feedback: Provides real-time feedback and confirmation of voice-activated actions, ensuring seamless interaction between the user and the database.
## Example Voice Command Workflow
1. User initiates voice recognition by clicking a button in the frontend.
2. The system listens for commands like:
   - "Create a new employee with name John Doe."
   - "Read all employees."
   - "Update employee ID 3 with name Jane Doe."
   - "Delete employee with ID 4."
3. The recognized text is sent to the API, which leverages OpenAI to generate an SQL query based on the voice command.
4. The SQL query is executed using the API's CRUD logic and the result is returned.
5. The user receives visual and/or auditory feedback indicating the success or failure of the operation.

### Getting Started
Clone the repository:
```sh
git clone -b master https://github.com/cikavelja/SampleStartProject.git

```

Navigate to the project directory and restore the dependencies:
```sh
cd your-repository
dotnet restore
```

***Update the database connection string to match your database configuration and OpenAI security key in appsettings.json.***  


Run the project:
```sh
dotnet run
```

### Aspire dashboard with OpenTelemetry  

https://learn.microsoft.com/en-us/samples/dotnet/aspire-samples/aspire-standalone-dashboard/

Prerequisites
.NET 8.0 SDK or later.  
An OpenAI API authentication key (https://platform.openai.com/api-keys).  
A database system (e.g., SQL Server, SQLite) for CRUD operations.  
Contributing  
Contributions are welcome!   
If you have any suggestions or improvements, feel free to create a pull request or open an issue.  

## License

Apache-2.0 license

**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>

   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
