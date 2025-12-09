using System;
using System.IO;
using ReviewSentiment.Core.Services;

namespace ReviewSentiment.Trainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) Training data (still relative to Trainer project)
            string dataPath = Path.Combine("data", "reviews.csv");

            // 2) Direct path to the API project folder (where we want model.zip)
            string apiProjectPath = @"C:\Users\mabuh\OneDrive\ASP_with_MachineLearning\ReviewSentimentSolution\ReviewSentiment.Api";

            // Make sure the folder exists
            if (!Directory.Exists(apiProjectPath))
            {
                Console.WriteLine("API project folder not found at:");
                Console.WriteLine(apiProjectPath);
                Console.ReadKey();
                return;
            }

            // 3) Final model path
            string modelPath = Path.Combine(apiProjectPath, "model.zip");

            Console.WriteLine($"Data path:  {Path.GetFullPath(dataPath)}");
            Console.WriteLine($"Model path: {modelPath}");

            if (!File.Exists(dataPath))
            {
                Console.WriteLine($"Training data not found at: {dataPath}");
                Console.ReadKey();
                return;
            }

            var service = new SentimentModelService();
            service.Train(dataPath, modelPath);

            Console.WriteLine();
            Console.WriteLine("******** TRAINING COMPLETE ********");
            Console.WriteLine("Model saved to:");
            Console.WriteLine(modelPath);

            Console.ReadKey();
        }
    }
}

