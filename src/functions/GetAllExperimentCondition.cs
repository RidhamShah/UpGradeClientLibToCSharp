using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Interfaces;
using Newtonsoft.Json;

public static class GetAllExperimentConditionClass
{
    public static async Task<IExperimentAssignmentv4[]> GetAllExperimentConditions(
        string url,
        string userId,
        string token,
        string clientSessionId,
        string context)
    {
        var data = new Dictionary<string, string>()
        {
            {"userId", userId},
            {"context", context}
        };

        var experimentConditionResponse = await FetchDataServiceClass.FetchDataService(url, token, clientSessionId, data, REQUEST_TYPES.POST);

        if (experimentConditionResponse.status)
        {
            var responseData = JsonConvert.DeserializeObject<IExperimentAssignmentv4[]>(experimentConditionResponse.data.ToString());
            return responseData;
        }
        else
        {
            throw new Exception(experimentConditionResponse.message.ToString());
        }
    }
}