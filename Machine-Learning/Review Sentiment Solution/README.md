# Review  Sentiment Solution
ML.NET + ASP.NET Core Web API + React UI - End-to-End Sentiment Analysis
ReviewSentimentSolution is a complete machine learning application that demonstrates how to build, train, deploy, and interact with a sentiment analysis model using ML.NET, ASP.NET Core Web API, and a modern React front-end UI.
This project analyzes product reviews and determines whether they are Positive or Negative, along with a confidence score.
## Solution Architecture
# ReviewSentimentSolution/
-   ReviewSentiment.Trainer/      # ML.NET model trainer (creates model.zip)
-   ReviewSentiment.Core/         # Shared schema + prediction models
-   ReviewSentiment.Api/          # ASP.NET Core Web API + UI hosting

##  Features
# Machine Learning (ML.NET)
-   FastTree Binary Classification model
-   Trained on labeled product reviews (reviews.csv)
-   Outputs:
   	    - Sentiment: Positive / Negative
            - Probability score (0-1)
# ASP.NET Core Web API
-   Loads the trained model.zip
-   Simple prediction endpoint:
-   POST /predict
-   Auto-hosts the built React UI inside wwwroot
-   Swagger support included (development mode)
## Modern React App
-   Full app layout:
-   Header & branding
-   Sidebar with sample reviews
-   Recent prediction history
-   Result card with sentiment badge
-   Responsive and clean design
-   Connects directly to /predict

## Getting Started
Prerequisites
- 	.NET 8 SDK
- 	Node.js 16+
- 	Visual Studio 2022(This version is what I used in my machine)
- 	Modern browser (Chrome/Edge)

## Train the ML Model (one time)
1.	In Visual Studio, set ReviewSentiment.Trainer as Startup Project
2.	Press F5
-   You should see:
-   Training model...
-   Accuracy: ...
-   F1 Score: ...
-   Model saved to: .../ReviewSentiment.Api/model.zip
-   Once model.zip is created, you can run the API.

## Run the ASP.NET Core Web API
1.	Set ReviewSentiment.Api as Startup Project
2.	Press F5
3.	The browser will automatically open: https://localhost:62866/

# This only shows:
- 	Review Sentiment API is running.
- 	IMPORTANT - Open the UI
- 	Replace the URL manually with: https://localhost:62866/ui


