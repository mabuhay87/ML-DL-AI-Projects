using System;
using System.IO;
using ReviewSentiment.Core.Services;

namespace ReviewSentiment.Trainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Paths (relative to Trainer project's output)
            string dataPath = Path.Combine("data", "reviews.csv");

            // Save model in API project root so it can be copied to output
            string solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string apiProjectPath = Path.Combine(solutionRoot, "ReviewSentiment.Api");
            string modelPath = Path.Combine(apiProjectPath, "model.zip");

            if (!File.Exists(dataPath))
            {
                Console.WriteLine($"Training data not found at: {dataPath}");
                return;
            }

            var service = new SentimentModelService();
            service.Train(dataPath, modelPath);

            Console.WriteLine("Training complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
