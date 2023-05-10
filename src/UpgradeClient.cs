using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;
using Enums;

namespace ClientLib
{
  public class UpgradeClient
  {
    // Endpoints URLs
    private Dictionary<string, string> api = new Dictionary<string, string> {
    { "init","" },
    { "getAllExperimentConditions", "" },
    { "markExperimentPoint", "" },
    { "setGroupMembership", "" },
    { "setWorkingGroup", "" },
    { "failedExperimentPoint", "" },
    { "getAllFeatureFlag", "" },
    { "log", "" },
    { "logCaliper", "" },
    { "altUserIds", "" },
    { "addMetrics", "" }
  };

    private string userId;
    private string hostUrl;
    private string context;
    private string token;
    private string clientSessionId;

    private Dictionary<string, List<string>> group = null;
    private Dictionary<string, string> workingGroup = null;
    // private List<IExperimentAssignmentv4> experimentConditionData = null;
    private IExperimentAssignmentv4[] experimentConditionData = null;
    private List<IFeatureFlag> featureFlags = null;

    public UpgradeClient(string userId, string hostUrl, string context, string token = null, string clientSessionId = null)
    {

      Console.WriteLine("Upgrade Client Constructor called with parameters: {0}, {1}, {2}", userId, hostUrl, context);

      this.userId = userId;
      this.hostUrl = hostUrl;
      this.context = context;
      this.token = token;
      this.clientSessionId = clientSessionId ?? Guid.NewGuid().ToString();

      this.api["init"] = $"{hostUrl}/api/v4/init";
      this.api["getAllExperimentConditions"] = $"{hostUrl}/api/v4/assign";
      this.api["markExperimentPoint"] = $"{hostUrl}/api/v4/mark";
      this.api["setGroupMembership"] = $"{hostUrl}/api/v4/groupmembership";
      this.api["setWorkingGroup"] = $"{hostUrl}/api/v4/workinggroup";
      this.api["failedExperimentPoint"] = $"{hostUrl}/api/v4/failed";
      this.api["getAllFeatureFlag"] = $"{hostUrl}/api/v4/featureflag";
      this.api["log"] = $"{hostUrl}/api/v4/log";
      this.api["logCaliper"] = $"{hostUrl}/api/v4/logCaliper";
      this.api["altUserIds"] = $"{hostUrl}/api/v4/useraliases";
      this.api["addMetrics"] = $"{hostUrl}/api/v4/metric";
    }

    private void ValidateClient()
    {
      if (string.IsNullOrEmpty(this.hostUrl))
      {
        throw new InvalidOperationException("Please set application host URL first.");
      }
      if (string.IsNullOrEmpty(this.userId))
      {
        throw new InvalidOperationException("Please provide valid user id.");
      }
    }

    public async Task<IUser> Init(Dictionary<string, List<string>> group = null, Dictionary<string, string> workingGroup = null)
    {
      ValidateClient();
      Console.WriteLine("Init function called in");
      
      IUser data = await InitClass.Init(this.api["init"], this.userId, this.token, this.clientSessionId, group, workingGroup);
      return data;
    }

    // public async Task < Interfaces.IUser > SetGroupMembership(Dictionary < string, List < string >> group) {
    //   ValidateClient();
    //   Interfaces.IUser response = await SetGroupMembership(this.api.SetGroupMembership, this.userId, this.token, this.clientSessionId, group);
    //   if (response.id != null) {
    //     this.group = group;
    //     response = new Interfaces.IUser {
    //       id = response.id,
    //         workingGroup = this.workingGroup
    //     };
    //   }
    //   return response;
    // }

    // public async Task < IUser > SetWorkingGroup(Dictionary < string, string > workingGroup) {
    //   ValidateClient();
    //   IUser response = await SetWorkingGroup(this.api.SetWorkingGroup, this.userId, this.token, this.clientSessionId, workingGroup);
    //   if (response.Id != null) {
    //     this.workingGroup = workingGroup;
    //     response.Group = this.group;
    //   }
    //   return response;
    // }

    public async Task < IExperimentAssignmentv4[] > GetAllExperimentConditions() {
      ValidateClient();
      IExperimentAssignmentv4[] response = await GetAllExperimentConditionClass.GetAllExperimentConditions(
        this.api["getAllExperimentConditions"],
        this.userId,
        this.token,
        this.clientSessionId,
        this.context
      );
      if (response != null) {
        this.experimentConditionData = response;
      }
      return response;
    }

    // public async Task < Assignment > GetDecisionPointAssignment(string site, string target) {
    //   ValidateClient();
    //   if (this.experimentConditionData == null) {
    //     await this.GetAllExperimentConditions();
    //   }
    //   return GetExperimentCondition(this.experimentConditionData, site, target);
    // }

    // public async Task < IMarkExperimentPoint > MarkExperimentPoint(string site, string target, string condition = null, MARKED_DECISION_POINT_STATUS status, string clientError = null) {
    //   ValidateClient();
    //   if (this.experimentConditionData == null) {
    //     await this.GetAllExperimentConditions();
    //   }
    //   return await MarkExperimentPoint(this.api.MarkExperimentPoint, this.userId, this.token, this.clientSessionId, site, target, condition, status, this.experimentConditionData, clientError);
    // }

    // public async Task < IFeatureFlag[] > GetAllFeatureFlags() {
    //   ValidateClient();
    //   IFeatureFlag[] response = await GetAllFeatureFlags(this.api.GetAllFeatureFlag, this.token, this.clientSessionId);
    //   if (response.Length > 0) {
    //     this.featureFlags = response;
    //   }
    //   return response;
    // }

    // public IFeatureFlag GetFeatureFlag(string key) {
    //   ValidateClient();
    //   return GetFeatureFlag(this.featureFlags, key);
    // }

    // public async Task < ILog[] > Log(List < ILogInput > value, bool sendAsAnalytics = false) {
    //   ValidateClient();
    //   return await Log(this.api.Log, this.userId, this.token, this.clientSessionId, value, sendAsAnalytics);
    // }

    // public async Task < ILog[] > LogCaliper(CaliperEnvelope value, bool sendAsAnalytics = false) {
    //   ValidateClient();
    //   return await LogCaliper(this.api.LogCaliper, this.userId, this.token, this.clientSessionId, value, sendAsAnalytics);
    // }

    // public async Task < IExperimentUserAliases[] > SetAltUserIds(string[] altUserIds) {
    //   ValidateClient();
    //   return await SetAltUserIds(this.api.AltUserIds, this.userId, this.token, this.clientSessionId, altUserIds);
    // }

    // public async Task < IMetric[] > AddMetrics(List < ISingleMetric > metrics) {
    //   ValidateClient();
    //   return await AddMetrics(this.api.AddMetrics, this.token, this.clientSessionId, metrics);
    // }

    // public async Task < IMetric[] > AddMetrics(List < IGroupMetric > metrics) {
    //   ValidateClient();
    //   return await AddMetrics(this.api.AddMetrics, this.token, this.clientSessionId, metrics);
    // }

  }
}