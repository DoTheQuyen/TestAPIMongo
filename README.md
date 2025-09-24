# DoTheQuyen Test API with MongoDB

This project is a simple API built using Node.js and MongoDB. It allows users to perform CRUD operations on a MongoDB database, providing a RESTful interface for interacting with the data.

## Features
- Create, Read, Update, and Delete operations
- RESTful API structure
- MongoDB integration

## Getting Started
To get started with this project, clone the repository and install the necessary dependencies:
- Software:
    Visual Studio 2022
    MongoDB
    MongoDB Compass
    NuGet CLI
- NuGet packages:
   # Main project packages
    dotnet add package FluentValidation
    dotnet add package FluentValidation.AspNetCore
    dotnet add package Swashbuckle.AspNetCore
    dotnet add package Serilog.AspNetCore
    dotnet add package Serilog.Sinks.File
    dotnet add package Serilog.Settings.Configuration

    # Test project packages
    dotnet add package NUnit
    dotnet add package NUnit3TestAdapter
    dotnet add package Microsoft.NET.Test.Sdk
    dotnet add package Moq  


## Usage

## To run the API:

Load the project from GitHub and open it in Microsoft Visual Studio.

Start the project by clicking the Run button.

Note: The system may require you to install the .NET CLI to trust the local development certificate. If prompted:

dotnet dev-certs https --clean   # Removes old certificates
dotnet dev-certs https --trust   # Generates a new certificate and trusts it


The API will run on https://localhost:7055 by default.

Open the Swagger interface in your browser. This lists all available APIs from the Controllers.

Expand the endpoint /Orders/get-orders-list and click Try it out.


## Request Body Instructions

- "PharmacyId": "string" → remove "string" and either leave it empty "" or provide a specific Pharmacy ID to search.

- "Status": "string" → same as above.

- "From" and "To" → remove if not required, or provide a valid datetime to filter by date.

- "PageNumber" and "PageSize" → must be valid values:

- PageNumber > 0

- PageSize between 20–100

After setting the parameters, click Execute. The results will appear under the Responses section.


## NUnit Tests

The project includes NUnit tests to:

Demonstrate how developers can perform self-testing after implementing a function.

Validate business rules and compare the function’s outcome with expected results.

Prevent accidental changes that may introduce bugs.

Test failures will raise a red flag for potential issues in the function.



## Logging

The system automatically logs actions and errors during operation.

Log files are stored in:

$TestAPIMongo\TestAPIMongo\logs



## Indexing / Query Approach

The query approach used is practical for production scenarios.



## Trade-offs & Next Steps

Due to time constraints, the following were not implemented:

Authentication and authorization logic – essential for public APIs.

Optimized code framework – improvements for maintainability and adherence to coding standards.

React front-end – I plan to build this, which I am confident in adapting.
