# Overview

Example PowerShell module resulting in an HTTP version issue in combination with Graph v4.x when being used in Windows PowerShell.

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
