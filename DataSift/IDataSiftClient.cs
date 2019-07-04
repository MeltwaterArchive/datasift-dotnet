using DataSift.Enum;
using DataSift.Rest;
using DataSift.Rest.Account;
using DataSift.Rest.Pylon;
using DataSift.Streaming;

namespace DataSift
{
    public interface IDataSiftClient
    {
        Account Account { get; }
        Historics Historics { get; }
        HistoricsPreview HistoricsPreview { get; }
        ODP ODP { get; }
        Push Push { get; }
        Pylon Pylon { get; }
        Source Source { get; }

        RestAPIResponse Balance();
        RestAPIResponse Compile(string csdl);
        DataSiftStream Connect(bool secure = true, string domain = "stream.datasift.com", bool autoReconnect = true);
        RestAPIResponse DPU(string hash = null, string historicsId = null);
        IIngestAPIRequest GetIngestRequest();
        IRestAPIRequest GetRequest();
        PullAPIResponse Pull(string id, int? size = default(int?), string cursor = null);
        RestAPIResponse Usage(UsagePeriod? period = default(UsagePeriod?));
        RestAPIResponse Validate(string csdl);
    }
}