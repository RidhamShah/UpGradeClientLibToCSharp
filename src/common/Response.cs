using Interfaces;

public class Response : IResponse
{
    public bool status { get; set; }
    public object data { get; set; }
    public object message { get; set; }
}