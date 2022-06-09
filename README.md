# Overview

Example PowerShell module resulting in an HTTP version error when reading groups using Microsoft.Graph v4.x when the module is being used in Windows PowerShell. The Cmdlet Test-GraphPowerShellHttpVersionIssue will use an application identity to get a token and then try to read all groups using the Graph client.

# Debugging

1. Create an Azure AD Application Registration that has the Microsoft Graph Group.Read.All application permission.
2. Start the project in Visual Studio and execute the following Cmdlet (replace the parameters, you will be prompted for the application secret once you run the command) in the opening PowerShell window:

```ps
Test-GraphPowerShellHttpVersionIssue -TenantId "TENANTID" -ApplicationId "APPLICATIONID" -ApplicationSecret (Read-Host -Prompt "Enter application secret" -AsSecureString)
```

- What works
  - Running the example with the 'PowerShell' profile when using Microsoft.Graph v3.x
  - Running the example with the 'Windows PowerShell' profile when using Microsoft.Graph v3.x
  - Running the example with the 'PowerShell' profile when using Microsoft.Graph v4.x
- What does not work
  - Running the example with the 'Windows PowerShell' profile when using Microsoft.Graph v4.x

# Debug Profiles

## PowerShell

Runs pwsh.exe (PowerShell 7.x) and imports the PowerShell module.

## Windows PowerShell

Runs powershell.exe (Windows PowerShell 5.x) and imports the PowerShell module.

# Error

The error happens when trying to get groups using the Graph client.

Exception message:

```
Code: generalException
Message: An error occurred sending the request.
```

Inner exception message:

```
WARNING: Only HTTP/1.0 and HTTP/1.1 version requests are currently supported.
Parameter name: value
```
