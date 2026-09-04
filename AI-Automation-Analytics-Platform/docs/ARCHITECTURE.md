# Architecture

## AI Automation & Analytics Platform

The AI Automation & Analytics Platform is designed as an enterprise-style web application that combines application development, artificial intelligence, data engineering, automation, and embedded business intelligence.

## High-Level Architecture

```text
┌──────────────────────────────────────────────┐
│                  Web Browser                 │
│         AI Automation & Analytics UI         │
└──────────────────────┬───────────────────────┘
                       │ HTTPS
                       ▼
┌──────────────────────────────────────────────┐
│            ASP.NET Core MVC / C#             │
│                                              │
│ Controllers  •  Models  •  Views  • Services│
│ Authentication • Business Logic • Validation│
└──────┬──────────┬───────────┬─────────┬──────┘
       │          │           │         │
       ▼          ▼           ▼         ▼
┌──────────┐ ┌──────────┐ ┌────────┐ ┌───────────────┐
│ OpenAI / │ │SQL Server│ │  SSIS  │ │Microsoft Entra│
│ Azure AI │ │          │ │ Catalog│ │      ID       │
└──────────┘ └──────────┘ └────────┘ └───────┬───────┘
                                             │
                                             ▼
                                   ┌──────────────────┐
                                   │ Power BI / Fabric│
                                   │ REST APIs + Embed│
                                   └──────────────────┘
```

## Application Layer

The front end is implemented using ASP.NET Core MVC Razor views. Controllers coordinate user requests and call application services rather than placing external integration logic directly in the UI.

Major application areas include:

- Authentication and access
- AI integration
- AI SQL generation
- AI chatbot
- Power BI analytics
- AI workflow generation
- Background jobs
- CSV analysis
- SSIS monitoring

## AI Integration Layer

The application can communicate with OpenAI/Azure AI through server-side services.

Example uses include:

- natural-language prompts
- SQL query generation
- technical assistant responses
- workflow-generation assistance
- data-analysis recommendations

API credentials are not stored in public source files.

## Data Layer

Microsoft SQL Server provides the relational data layer for application and analytics scenarios.

The platform can support:

- application data
- analytics staging data
- ETL data
- operational tables
- stored procedures
- views
- SSIS catalog information

Entity Framework Core or direct SQL access can be used depending on the application component.

## SSIS Monitoring

The SSIS monitoring feature reads execution information from the SQL Server Integration Services catalog.

Example source:

```text
SSISDB.catalog.executions
```

The UI can display execution counts, succeeded/failed status, package information, and execution timestamps.

## Power BI Embedded Architecture

Power BI reports are integrated into the ASP.NET Core application using an **app-owns-data / service-principal-style architecture**.

Conceptual flow:

```text
User Browser
     |
     v
ASP.NET Core Application
     |
     | 1. Server authenticates securely
     v
Microsoft Entra ID
     |
     | 2. Application access token
     v
Power BI REST API / Microsoft Fabric
     |
     | 3. Retrieve report information
     | 4. Generate embed token
     v
ASP.NET Core Application
     |
     | 5. Send only required embed configuration
     v
Power BI JavaScript Client
     |
     v
Interactive Embedded Report
```

The browser does **not** receive the application's client secret.

## Power BI Report Catalog

The analytics portal provides a central report-selection interface. Current reports include:

- Healthcare Claims Denial Intelligence
- Sales & Customer Growth Intelligence

Future reports can be added to the same portal without redesigning the overall application.

## Authentication Architecture

The application includes authenticated user access and a user directory. Authorization can be extended into role-based policies such as:

```text
Administrator
Analyst
Viewer
```

This architecture can later be connected to more advanced Microsoft Entra ID authentication or enterprise identity policies.

## Background Jobs

The Automation Job Center represents scheduled or manually triggered operational processes such as:

- Daily Data Refresh
- Claims Quality Check
- Power BI Health Check
- SSIS Status Scan

The architecture can be extended with hosted services, Hangfire, Azure Functions, Logic Apps, or other scheduling/orchestration technologies.

## Deployment Architecture

A production deployment could use:

```text
Azure App Service
       |
       +-- ASP.NET Core Application
       |
       +-- Azure Key Vault
       |
       +-- Azure SQL / SQL Server
       |
       +-- Microsoft Entra ID
       |
       +-- Microsoft Fabric / Power BI
       |
       +-- Azure OpenAI / OpenAI
```

Secrets should be provided through secure configuration providers rather than committed configuration files.

## Design Goals

The architecture emphasizes:

- separation of concerns
- server-side secret handling
- reusable services
- secure embedded analytics
- modular feature expansion
- enterprise-style authentication
- integration between AI and BI
- maintainable C# application design
