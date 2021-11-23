namespace ClanManagerApi.Models.Enums
{
    /// <summary>
    /// Known reasons for failure, that can dictate app behavior
    /// </summary>
    public enum ResultType
    {
        Success,
        NotFound,
        Conflict,
        Other
    }
}
