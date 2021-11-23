using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClanManagerApi.Models
{
    public record ShoppingListItem
    {
        public string ItemName { get; set; }
        public bool IsComplete { get; set; }
    }
}
