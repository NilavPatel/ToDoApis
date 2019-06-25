# Generic repository pattern in Asp.Net Core

## Types of base repository
````
1.  Sync generic Repository
2.  Aysnc generic Repository
````

## Features
````
1.  CRUD operations with repsository pattern
2.  Use stored procedure and query in repository
3.  Specification pattern
4.  Logger Factory with NLog implementation
5.  Custom Exception filter attribute
````

## How to get HttpContext in Asp.net core
````
1. In Startup.cs file add below code

    services.AddScoped< Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

2. In controller or service class pass dependency as 

    private HttpContext _currentHttpContext;
    public Class_Constructor(IHttpContextAccessor httpContextAccessor){
        _currentHttpContext = httpContextAccessor.HttpContext;
    }
````