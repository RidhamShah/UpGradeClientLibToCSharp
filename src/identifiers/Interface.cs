using Enums;

namespace Interfaces
{
  public interface IExperimentAssignmentv4
  {
    string Site { get; set; }
    string Target { get; set; }
    AssignedCondition AssignedCondition { get; set; }
    IDictionary<string, AssignedFactor> AssignedFactor { get; set; }
  }

  public class AssignedCondition
  {
    public string ConditionCode { get; set; }
    public IPayload Payload { get; set; }
    public string ExperimentId { get; set; }
    public string Id { get; set; }
  }

  public class AssignedFactor
  {
    public string Level { get; set; }
    public IPayload Payload { get; set; }
  }

  public interface IFlagVariation
  {
    string Id { get; set; }
    string Value { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    bool[] DefaultVariation { get; set; }
  }

  public interface IFeatureFlag
  {
    string Id { get; set; }
    string Name { get; set; }
    string Key { get; set; }
    string Description { get; set; }
    string VariationType { get; set; }
    bool Status { get; set; }
    IList<IFlagVariation> Variations { get; set; }
  }

  public interface ILogMetrics
  {
    IDictionary<string, string> Attributes { get; set; }
    IList<ILogGroupMetrics> GroupedMetrics { get; set; }
  }

  public interface ILogGroupMetrics
  {
    string GroupClass { get; set; }
    string GroupKey { get; set; }
    string GroupUniquifier { get; set; }
    IDictionary<string, string> Attributes { get; set; }
  }

  public interface ILogInput
  {
    string Timestamp { get; set; }
    ILogMetrics Metrics { get; set; }
  }

  public interface IGroupMetric
  {
    string GroupClass { get; set; }
    IList<string> AllowedKeys { get; set; }
    IList<IMetric> Attributes { get; set; }
  }

  public interface ISingleMetric : IMetric
  {
    string Metric { get; set; }
    IMetricMetaData Datatype { get; set; }
    IList<object> AllowedValues { get; set; }
  }

  public interface IMetric
  {
  }

  public interface IPayload
  {
    PAYLOAD_TYPE Type { get; set; }
    string Value { get; set; }
  }

  public interface IUser
  {
    string Id { get; set; }
    Dictionary<string, List<string>> Group { get; set; }
    Dictionary<string, string> WorkingGroup { get; set; }
  }


  public interface IResponse
  {
    bool status { get; set; }
    object data { get; set; }
    object message { get; set; }
  }
}