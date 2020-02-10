namespace GC.DataExchange.Providers.Json.Pipelines
{
    public class ReadPaginatedJsonApiStepSettings : ReadJsonApiStepSettings
    {
        public string Page { get; set; }
        public string Offset { get; set; }
        public string ResultsPerPage { get; set; }
    }
}
