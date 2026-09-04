# AI Automation & Analytics Platform

An enterprise-style portfolio application built with **ASP.NET Core MVC, C#, SQL Server, AI services, SSIS monitoring, and Microsoft Power BI Embedded**.

The platform brings AI-assisted development, data automation, operational monitoring, authentication, and interactive business intelligence into a single web portal.

> **Portfolio note:** This public repository contains documentation and screenshots of the application. Security-sensitive configuration and the configured Visual Studio project are intentionally excluded.

## Project Highlights

- AI Automation Portal with authenticated access
- OpenAI / Azure AI integration
- AI-generated SQL queries
- AI chatbot assistant for SQL, Power BI, SSIS, C#, and automation questions
- Power BI Embedded analytics
- Microsoft Fabric / Power BI Service integration
- AI Agent Workflow Designer
- Authentication and application-user access
- Background automation job center
- CSV AI analysis
- SSIS execution monitoring
- SQL Server integration

## Embedded Power BI Reports

### Healthcare Claims Denial Intelligence
Enterprise healthcare analytics including denial KPIs, denial drivers, provider performance, AI-predicted claim risk, work queues, and model monitoring.

### Sales & Customer Growth Intelligence
Sales analytics covering revenue, gross profit, margin, customer growth and retention, product performance, regional/channel performance, and forecasting.

Additional portfolio reports are under development.

## Technology Stack

| Area | Technology |
|---|---|
| Application | ASP.NET Core MVC / C# / .NET |
| Development | Visual Studio 2022 |
| Database | Microsoft SQL Server |
| AI | OpenAI / Azure AI integration |
| Business Intelligence | Microsoft Power BI |
| Embedded Analytics | Power BI Embedded |
| Cloud BI Platform | Microsoft Fabric / Power BI Service |
| Authentication | ASP.NET Core application authentication |
| Power BI Authentication | Microsoft Entra ID service principal |
| ETL / Monitoring | SQL Server Integration Services (SSIS) |
| Security | ASP.NET Core User Secrets / environment-based configuration |

```

## Architecture

The application uses a layered ASP.NET Core MVC architecture. Browser requests are handled by MVC controllers and services, while integrations with OpenAI/Azure AI, SQL Server, SSIS, Microsoft Entra ID, Microsoft Fabric, and Power BI are performed server-side.

Power BI reports are displayed inside the application through **Power BI Embedded** rather than simply linking users to Power BI Service.

See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md).

## Features

Detailed feature documentation is available in [docs/FEATURES.md](docs/FEATURES.md).

## Security

No API keys, client secrets, passwords, database credentials, connection strings, access tokens, MFA QR codes, or production configuration are published.

See [docs/SECURITY.md](docs/SECURITY.md).

## Screenshots

The screenshots in this repository demonstrate the working application without exposing the security-sensitive Visual Studio configuration.

Recommended screenshots include:

1. AI Automation Portal login
2. AI Automation Main Menu
3. OpenAI / Azure AI integration
4. AI-generated SQL queries
5. AI Chatbot Assistant
6. Power BI Analytics Portal
7. Healthcare Claims Denial Intelligence embedded report
8. Sales & Customer Growth Intelligence embedded report
9. AI Agent Workflow Designer
10. Authentication & Access
11. Background Jobs
12. CSV AI Analysis
13. SSIS Execution Monitor

## Why Source Configuration Is Not Published

This application connects to external AI, Microsoft cloud(Microsoft Fabric), database, and analytics services. The local Visual Studio solution uses security-sensitive configuration that must never be committed to a public repository.

The repository therefore demonstrates the solution through **architecture documentation, feature documentation, security documentation, and application screenshots** while keeping secrets and private configuration outside source control.

## Portfolio Purpose

This project demonstrates the ability to combine:

**Software Engineering + Artificial Intelligence + SQL/ETL + Business Intelligence + Embedded Analytics + Enterprise Security**

into one integrated application.

---
### Note:

This project is intended to demonstrate the architecture and capabilities of
an enterprise-style solution combining software engineering, artificial
intelligence, data engineering, automation, and embedded business intelligence.

## Author

Developed by Cristina L. Fontenot



**Status:** Active portfolio project — additional Power BI reports and automation capabilities are being developed.

