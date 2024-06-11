namespace Domain.Common;
public class ActionResponse<T> : IActionResponse
{
    public ActionResponse()
    {
        Errors = new List<string>();
    }

    public int Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public ResponseStateEnum State { get; set; }
    public List<string> Errors { get; set; }
}
public class ActionResponse : IActionResponse
{
    public ActionResponse()
    {
        Errors = new List<string>();
    }

    public ResponseStateEnum State { get; set; }
    public List<string> Errors { get; set; }
}
public interface IActionResponse
{
    ResponseStateEnum State { get; set; }
    List<string> Errors { get; set; }
}
public enum ResponseStateEnum
{
    FAILED = 0,
    SUCCESS = 1,
    NOTAUTHORIZED = 2
}