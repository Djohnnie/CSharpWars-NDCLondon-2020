# CSharpWars

![CSharp Wars Logo](https://www.djohnnie.be/csharpwars/logo.png "CSharp Wars Logo")

[Return to README](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020)

[Return to step 2](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020/blob/master/workshop/step02/step.md)

## Step 3

Return to the backend solution in Visual Studio 2019 and make the backend webapi and processing middleware run successfully.

### CSharpWars.Web.Api

* Right-click the *CSharpWars.Web.Api* project and choose the *Properties* option.
* Select the *Debug* tab and add the following environment variables:

| Name | Value |
|------|-------|
| CONNECTION_STRING | dskfslkjfjkdsf |
| ARENA_SIZE | 10 |

* Open the newly created *launchSettings.json* file in the Properties node for the project.
* Remove the *IIS Express* profile and keep the *CSharpWars.Web.Api* profile

```json
{
  "profiles": {
    "CSharpWars.Web.Api": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "CONNECTION_STRING":  "",
        "ARENA_SIZE": "10" 
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
```

### CSharpWars.Processor

* Right-click the *CSharpWars.Web.Api* project and choose the *Properties* option.
* Select the *Debug* tab and add the following environment variables:

| Name | Value |
|------|-------|
| CONNECTION_STRING | dskfslkjfjkdsf |
| ARENA_SIZE | 10 |

[Continue to step 4](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020/blob/master/workshop/step04/step.md)