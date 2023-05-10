using Interfaces;

public class User : IUser
{
    public string Id { get; set; }
    public Dictionary<string, List<string>> Group { get; set; }
    public Dictionary<string, string> WorkingGroup { get; set; }
}