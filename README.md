This is an API built around the mock API called [REST API - Ready to use](https://restful-api.dev/).

This project extends that API functionality to only deal with it's own created objects, that in this case are products.

The mock API accepts custom data, but we need to store it somewhere. To keep the project simple, I chose to not use a database, so all of it's created objects ID's are stored in memory.

At each restart of this API, all the created product ID's will be wiped, requiring re-registration of the products.

Dependencies:
- [Visual Sudio 2022](https://visualstudio.microsoft.com/pt-br/vs/)
- [.NET Framework 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Further API usage instruction can be found on the generated Swagger Docs in development mode.
