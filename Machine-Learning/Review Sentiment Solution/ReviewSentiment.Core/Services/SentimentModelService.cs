using System;
using System.IO;
using Microsoft.ML;
using ReviewSentiment.Core.Models;

namespace ReviewSentiment.Core.Services
{
    public class SentimentModelService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private PredictionEngine<ReviewData, ReviewPrediction>? _predictionEngine;

        public SentimentModelService()
        {
            _mlContext = new MLContext(seed: 123);
        }

        public ITransformer Train(string trainingDataPath, string modelPath)
        {
            if (!File.Exists(trainingDataPath))
            {
                throw new FileNotFoundException("Training data file not found.", trainingDataPath);
            }

            // 1. Load data
            IDataView dataView = _mlContext.Data.LoadFromTextFile<ReviewData>(
                path: trainingDataPath,
                hasHeader: true,
                separatorChar: ',');

            // 2. Build pipeline
            var pipeline = _mlContext.Transforms.Text.FeaturizeText(
                                outputColumnName: "Features",
                                inputColumnName: nameof(ReviewData.Text))
                          .Append(_mlContext.BinaryClassification.Trainers.FastTree(
                                labelColumnName: nameof(ReviewData.Label),
                                featureColumnName: "Features"));

            // 3. Train
            Console.WriteLine("Training model...");
            _model = pipeline.Fit(dataView);

            // 4. Evaluate
            var predictions = _model.Transform(dataView);
            var metrics = _mlContext.BinaryClassification.Evaluate(
                predictions, labelColumnName: nameof(ReviewData.Label));

            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"AUC: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:P2}");

            // 5. Save model
            _mlContext.Model.Save(_model, dataView.Schema, modelPath);
            Console.WriteLine($"Model saved to: {modelPath}");

            return _model;
        }

        public void LoadModel(string modelPath)
        {
            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Model file not found", modelPath);

            DataViewSchema modelSchema;
            _model = _mlContext.Model.Load(modelPath, out modelSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ReviewData, ReviewPrediction>(_model);
        }

        public ReviewPrediction Predict(string text)
        {
            if (_predictionEngine == null)
                throw new InvalidOperationException("Model is not loaded. Call LoadModel first.");

            var input = new ReviewData { Text = text };
            return _predictionEngine.Predict(input);
        }
    }
}
