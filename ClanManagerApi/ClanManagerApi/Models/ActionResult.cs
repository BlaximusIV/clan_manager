using ClanManagerApi.Models.Enums;

namespace ClanManagerApi.Models
{
    public record ActionResult
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; } = "Success";
        public ActionResult(string message) => Message = message;
        public ActionResult(ResultType resultType, string message)
        {
            ResultType = resultType;
            Message = message;
        }
    }
}
