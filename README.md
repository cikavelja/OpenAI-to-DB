# Sample Project with UnitOfWork, OpenTelemetry, and OpenAI Integration

### Overview
This repository contains a sample project demonstrating the use of the Unit of Work pattern, OpenTelemetry for distributed tracing, and integration with the OpenAI API. The project is structured in two branches:

## Branches
- **master**: Contains the core implementation of the project with UnitOfWork and OpenTelemetry integration.
- **OpenAI_CRUD**: Extends the core implementation by integrating the OpenAI API and adding CRUD operations to a database, along with some mashup functionalities.  

### Master Branch
- **UnitOfWork Pattern**: Implements the UnitOfWork pattern to manage database transactions efficiently. This pattern helps in maintaining a list of operations that need to be executed within a transactional scope.
- **OpenTelemetry**: Integrates OpenTelemetry to provide distributed tracing and monitoring capabilities. This helps in tracking the application's performance and tracing requests through the system.  

### OpenAI_CRUD Branch
This branch extends the functionality of the master branch by adding:
- **OpenAI API Integration**: Connects to the OpenAI API to utilize its language models for various text processing tasks.
- **CRUD Operations**: Adds basic Create, Read, Update, and Delete operations for interacting with a database.
- **Mashup Functionalities**: Combines OpenAI's capabilities with CRUD operations, allowing for enhanced data processing and manipulation.

## Features
* Full CRUD implementation with a database of your choice (e.g., SQL Server, SQLite).
* Integration with the OpenAI API for text generation and processing.
* Example mashup of CRUD operations with OpenAI responses.

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

***Update the database connection string in appsettings.json to match your database configuration.***  

***Update the OpenAI security key in Sample.API.Infrastructure.External.API.AI.open.ai.GetAISQLResponse (line 9)***  

Run the project:
```sh
dotnet run
```
Prerequisites
.NET 8.0 SDK or later.
An OpenAI API key for the OpenAI_CRUD branch.
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
