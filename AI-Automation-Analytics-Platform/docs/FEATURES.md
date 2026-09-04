# Features

## AI Automation & Analytics Platform

This document describes the major capabilities demonstrated by the portfolio application.

## 1. Authentication & Access

The application starts with an authenticated login experience and provides an application user directory.

Demonstrated capabilities:

- username/password authentication
- authenticated sessions
- user-role display
- administrator access model
- application-user directory
- logout functionality
- protection of application features from unauthenticated access

The authorization architecture can be expanded to Administrator, Analyst, and Viewer roles.

## 2. AI Automation Main Menu

The main portal provides one entry point for the application's AI, analytics, automation, and monitoring capabilities.

Modules include:

- OpenAI / Azure AI
- AI SQL Queries
- AI Chatbot
- Power BI Insights
- AI Agent Workflows
- Authentication
- Background Jobs
- CSV AI Analysis
- SSIS Monitoring

## 3. OpenAI / Azure AI Integration

Provides a UI for testing server-side AI connectivity and submitting natural-language prompts.

Portfolio skills demonstrated:

- C# service integration
- asynchronous API calls
- configuration management
- prompt submission
- response handling
- separation of API credentials from source code

## 4. AI-Generated SQL Queries

Converts natural-language requests into SQL suggestions.

Example request:

```text
Generate SQL to find duplicate beneficiary IDs.
```

The AI can return a query using SQL concepts such as:

- SELECT
- GROUP BY
- HAVING
- COUNT
- ORDER BY

This module demonstrates how generative AI can assist database developers and analysts while keeping query execution separate from query generation.

## 5. AI Chatbot Assistant

A technical assistant designed around the platform's technology stack.

Example subject areas:

- SQL Server
- Power BI
- SSIS
- C#
- AI automation
- analytics

The feature demonstrates conversational AI integration inside an ASP.NET Core business application.

## 6. Power BI Analytics Portal

Provides a centralized embedded analytics catalog inside the AI Automation Dashboard.

Current reports:

### Healthcare Claims Denial Intelligence

Capabilities demonstrated include:

- executive denial KPIs
- total claims
- total billed
- total paid
- denial rate
- predicted high-risk dollars
- denial-reason analysis
- provider/plan performance
- AI-predicted claim risk
- work-queue analytics
- model monitoring

### Sales & Customer Growth Intelligence

Capabilities demonstrated include:

- revenue
- gross profit
- gross margin
- revenue growth
- monthly performance trends
- product/category performance
- regional performance
- customer segmentation
- customer growth and retention
- channel performance
- sales forecasting

Additional Power BI portfolio reports are under development.

## 7. Power BI Embedded

Reports are displayed directly inside the ASP.NET Core application.

The integration demonstrates:

- Microsoft Entra ID application registration
- service-principal authentication
- Power BI REST API integration
- workspace/report configuration
- server-side embed-token generation
- Power BI JavaScript embedding
- Microsoft Fabric / Power BI Service integration
- multi-report portal architecture

Sensitive authentication configuration is not included in the public repository.

## 8. AI Agent Workflow Designer

Allows a user to describe a repetitive business process in natural language and generate an automation-oriented workflow.

Example:

```text
Every morning check failed SSIS packages, summarize the errors,
create a support task, and notify the BI team.
```

Potential workflow outputs can include:

- trigger
- data source
- validation steps
- AI analysis
- business rules
- notifications
- exception handling
- audit logging

## 9. Background Jobs / Automation Job Center

Provides a portfolio UI for operational jobs.

Examples:

- Daily Data Refresh
- Claims Quality Check
- Power BI Health Check
- SSIS Status Scan

The interface includes recent-run history and can be expanded to persistent job scheduling and execution.

## 10. CSV AI Analysis

Allows a CSV file to be uploaded for data preview and analyst-level recommendations.

Potential analysis includes:

- data-quality observations
- missing-value detection
- duplicate detection
- column profiling
- KPI suggestions
- Power BI visualization recommendations
- AI-generated findings

## 11. SSIS Execution Monitor

Reads SQL Server Integration Services execution information and presents operational status through the web application.

Displayed information can include:

- executions loaded
- succeeded executions
- failed executions
- execution ID
- folder
- project
- package
- status
- start time
- end time

This demonstrates integration between ASP.NET Core and the SSIS catalog.

## 12. Enterprise Extension Opportunities

The architecture is designed for future capabilities such as:

- Azure Key Vault
- Azure App Service deployment
- Microsoft Entra ID user sign-in
- role-based authorization policies
- Row-Level Security (RLS)
- Power BI effective identities
- audit logging
- SQL-backed report catalog
- scheduled jobs
- email/Teams notifications
- AI-generated Power BI insights
- semantic-model metadata analysis
- application telemetry
