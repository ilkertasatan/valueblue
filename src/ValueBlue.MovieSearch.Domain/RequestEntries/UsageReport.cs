namespace ValueBlue.MovieSearch.Domain.RequestEntries
{
    public class UsageReport
    {
        public UsageReport(string timestamp, int count)
        {
            Timestamp = timestamp;
            Count = count;
        }
        
        public string Timestamp { get; }
        public int Count { get; }
    }
}