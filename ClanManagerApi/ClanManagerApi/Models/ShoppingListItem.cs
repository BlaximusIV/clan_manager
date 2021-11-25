namespace ClanManagerApi.Models
{
    public record ShoppingListItem
    {
        public string ItemName { get; set; }
        public bool IsComplete { get; set; }
    }
}
