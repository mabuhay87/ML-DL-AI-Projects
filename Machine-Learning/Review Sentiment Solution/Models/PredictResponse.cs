namespace ReviewSentiment.Api.Models
{
    public class PredictResponse
    {
        public string Text { get; set; } = string.Empty;
        public bool IsPositive { get; set; }
        public float Probability { get; set; }
    }
}
