namespace GC.DataExchange.Providers.Json.Pipelines
{
    public class ReadPaginatedJsonApiStepSettings : ReadJsonApiStepSettings
    {
        public int Page { get; set; }
        public int Offset { get; set; }
        public int ResultsPerPage { get; set; }
    }
}
