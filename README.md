# Overview

This is a job interview task.<br>
Application allows user to view list of contacs and see their details<br> 
After authentication it is possible to edit, delete and add new contacts<br>
There is a default user built into application with username: admin and password: Admin123.<br>
Application consists of backend in C#
and frontend in Angular using Nx workspace

# Setup and run
### Api
You need to provide a connection string to database and secret key for jwt generation for server/Contacto/Contacto.Api project.<br>
1. ConnectionStrings:ContactoDatabase<br>
2. Jwt:Secret<br>

You can set values for these in appsettings.json or in UserSecrets for this project.<br>
Example Secret value
```
dsfdsbhrehrtyhrthdtfhdfvdsfvfcdsvdfsbgdsbvgdsvsdvgeavdsxvcfsASDwaevdsv123.,/
```
You also have to execute migrations on a database<br>
I do it using Package Manager Console in Microsoft Visual Studio<br>
1. Open solution `server\Contacto\Contacto.sln`
2. Set startup project to Contacto.Api
3. In Package Manager Console set Default project to Contacto.Infrastructure
4. Run `update-database`


After migrations passed you can start the app in `https` launch-profile<br>
From root directory
```
cd server\Contacto\Contacto.Api
dotnet run --launch-profile https
```


### Frontend app
From root directory
```
cd client/contacto-portal
npm ci
npx nx serve
```

## External libraries
1. Backend
    - Mediatr
    - EntityFrameworkCore
    - Xunit
2. Frontend
    - angular
    - nx
    - @auth0/angular-jwt

