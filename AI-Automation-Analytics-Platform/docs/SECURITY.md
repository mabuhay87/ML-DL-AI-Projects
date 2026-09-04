# Security

## Security Overview

Security-sensitive information from the AI Automation & Analytics Platform is intentionally excluded from the public GitHub repository.

The public portfolio is designed to demonstrate the application's architecture and functionality without publishing credentials or private cloud configuration.

## Public Repository Policy

The repository may contain:

- README documentation
- architecture documentation
- feature descriptions
- security documentation
- sanitized screenshots
- non-sensitive diagrams

The repository must **not** contain:

- OpenAI API keys
- Azure OpenAI keys
- Microsoft Entra client secrets
- Power BI access tokens
- Power BI embed tokens
- database passwords
- private SQL Server connection strings
- User Secrets contents
- MFA QR codes
- authentication cookies
- production certificates/private keys
- `.env` files containing credentials
- sensitive tenant or infrastructure configuration

## ASP.NET Core User Secrets

During local Visual Studio 2022 development, sensitive configuration can be stored using **ASP.NET Core User Secrets**.

Conceptual example only:

```json
{
  "OpenAI": {
    "ApiKey": "NOT-STORED-IN-GITHUB"
  },
  "PowerBI": {
    "TenantId": "CONFIGURED-LOCALLY",
    "ClientId": "CONFIGURED-LOCALLY",
    "ClientSecret": "NOT-STORED-IN-GITHUB",
    "WorkspaceId": "CONFIGURED-LOCALLY",
    "ReportId": "CONFIGURED-LOCALLY"
  }
}
```

This example contains placeholders only.

User Secrets are intended for local development and should not be treated as a production secret store.

## Power BI Embedded Security

The Power BI Embedded integration uses server-side authentication.

Security design:

1. The ASP.NET Core server authenticates to Microsoft Entra ID.
2. The client secret remains on the server.
3. The server calls the Power BI/Fabric APIs.
4. The server obtains the information required to embed the authorized report.
5. The browser receives only the embed configuration required for the session.
6. The client secret is never sent to browser JavaScript.

Embed tokens are temporary credentials and must also be protected.

## Microsoft Entra ID

The Power BI integration uses a registered Microsoft Entra application/service principal.

Production security should follow least-privilege principles:

- grant only required API/tenant capabilities
- grant only required workspace access
- rotate client secrets
- remove expired secrets
- avoid sharing credentials
- monitor application/service-principal activity

## OpenAI / Azure AI Security

AI API credentials should be retrieved from secure server-side configuration.

They must never be:

- hard-coded in controllers
- hard-coded in Razor pages
- embedded in JavaScript
- committed to Git
- included in screenshots
- written to normal application logs

## SQL Server Security

Database connection strings may contain server names, usernames, passwords, or other infrastructure details.

Recommended practices:

- use secure configuration providers
- prefer integrated/managed identity where supported
- use least-privilege database accounts
- parameterize SQL commands
- validate uploaded/input data
- restrict SSIS catalog access
- avoid displaying connection details in error messages

## Git Security

Recommended `.gitignore` coverage includes:

```gitignore
# Visual Studio
.vs/
bin/
obj/

# Local secrets / environment
.env
.env.*
secrets.json
appsettings.Development.json

# User-specific files
*.user
*.suo

# Logs
logs/
*.log
```

Important: `appsettings.json` can be committed only when it contains safe defaults/placeholders and no credentials.

## Screenshot Security Checklist

Before uploading a screenshot to GitHub, verify that it does not expose:

- API keys
- client-secret values
- passwords
- connection strings
- access/embed tokens
- MFA QR codes
- authentication cookies
- browser developer-tool authorization headers
- private customer/member/patient information
- personally identifiable information
- confidential production data

Crop or redact anything sensitive before publishing.

## Healthcare Data

The Healthcare Claims Denial Intelligence report is a portfolio analytics project.

Public portfolio screenshots should use synthetic, anonymized, or otherwise non-sensitive demonstration data. Real protected health information (PHI) should never be published to GitHub.

## Production Secret Management

For a production Azure deployment, use a dedicated secret-management solution such as:

```text
Azure Key Vault
     |
     v
ASP.NET Core Configuration
     |
     +-- AI credentials
     +-- database credentials
     +-- Power BI / Entra credentials
```

Managed identities should be preferred where supported.

## Error Handling and Logging

Production applications should:

- log operational events without logging secrets
- avoid returning internal exception details to end users
- use centralized telemetry
- protect logs with appropriate access controls
- record security-relevant events
- monitor repeated authentication failures

## Secret Rotation

If a secret is accidentally committed to GitHub, simply deleting it from the latest file is not enough.

The credential should be:

1. revoked/rotated immediately
2. replaced in the secure configuration store
3. removed from Git history where appropriate
4. checked for unauthorized use

## Portfolio Publishing Strategy

For this project, the public GitHub version focuses on:

**Screenshots + Architecture + Features + Security Documentation**

This provides evidence of the working application while preventing disclosure of security-sensitive configuration.

---

**Security principle:** Demonstrate the engineering. Do not publish the credentials that make it run.
