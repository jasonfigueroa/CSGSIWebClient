# Requirements

- bower cli
- dotnet version 2.1 or greater

# Get Up and Running

1. Clone repo
2. Cd into the CSGSIWebClient subdirectory
3. Run `bower install`
4. Run `dotnet restore`
5. In CSGSIWebClient/wwwroot/js/site.js change the basUrl constant to the development URL, ex. http://localhost:5000
6. Run `dotnet run`