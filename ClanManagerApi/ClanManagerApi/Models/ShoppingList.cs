using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ClanManagerApi.Models.Enums;

namespace ClanManagerApi.Models
{
    /// <summary>
    /// A wrapping class to allow for singleton-level access to the underlying shopping list
    /// </summary>
    public class ShoppingList
    {
        private readonly ConcurrentDictionary<string, bool> _shoppingList;

        public ShoppingList() => _shoppingList = new ConcurrentDictionary<string, bool>();

        /// <summary>
        /// Retrieves the entire contents of the shopping list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShoppingListItem> GetAllItems() => _shoppingList
            .Select(kv => new ShoppingListItem { ItemName = kv.Key, IsComplete = kv.Value });

        public ActionResult TryAddItem(string item)
        {
            if (_shoppingList.ContainsKey(item))
                return new ActionResult(ResultType.Conflict, $"{item} already in the shopping list");

            var isSuccessful = _shoppingList.TryAdd(item, false);

            var resultType = isSuccessful ? ResultType.Success : ResultType.Other;
            var returnMessage = isSuccessful ? "Success" : "Unable to add item";

            return new ActionResult(resultType, returnMessage);
        }

        public ActionResult TryToggleItemCompletion(string item)
        {
            var isSuccessful = _shoppingList.TryGetValue(item, out bool value);

            if (!isSuccessful)
                return NotFoundResult;

            isSuccessful = _shoppingList.TryUpdate(item, !value, value);

            if (!isSuccessful)
                return new ActionResult(ResultType.Other, "Unable to update item");

            return new ActionResult("Success");
        }

        public ActionResult TryRenameItem(string item, string newName)
        {
            // I realize this is the same logic from above, but the 3 lines don't 
            // seem to justify an abstracting method
            var isSuccessful = _shoppingList.TryGetValue(item, out bool value);

            if (!isSuccessful)
                return NotFoundResult;

            isSuccessful = _shoppingList.TryRemove(_shoppingList.Single(kv => kv.Key == item));

            if (!isSuccessful)
                return new ActionResult(ResultType.Other, "Unable to remove item from list");

            isSuccessful = _shoppingList.TryAdd(newName, value);

            if (!isSuccessful)
                // If the previous action is successful and this one fails, we've just a key/value in the dictionary
                return new ActionResult(ResultType.Other, "Unable to add item to list");

            return new ActionResult("Success");
        }


        public ActionResult TryDeleteItem(string item)
        {
            if (!_shoppingList.ContainsKey(item))
                return NotFoundResult;

            var isSuccessful = _shoppingList.TryRemove(_shoppingList.Single(kv => kv.Key == item));

            if (!isSuccessful)
                return new ActionResult(ResultType.Other, "Unable to remove item from list");

            return new ActionResult("Success");
        }

        public void RemoveItems() => _shoppingList.Clear();

        #region Private Methods

        private ActionResult NotFoundResult => new ActionResult(ResultType.NotFound, "Unable to locate item");

        #endregion
    }
}
