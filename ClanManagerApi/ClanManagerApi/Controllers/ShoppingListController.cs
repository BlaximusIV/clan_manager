using ClanManagerApi.Models;
using ClanManagerApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClanManagerApi.Controllers
{
    /// <summary>
    /// Controller to manage items on the shared shopping list
    /// </summary>
    [Route("[controller]")]
    [Authorize(Policy = "User")]
    [ApiController]
    public class ShoppingListController : Controller
    {
        private readonly ShoppingList _shoppingList;

        // I realize typically it's injected by interface. This is such a small application though, I don't mind depending on the implementation.
        public ShoppingListController(ShoppingList shoppingList) => _shoppingList = shoppingList;

        /// <summary>
        /// Retrieve all items in the shopping list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList()
        {
            var items = _shoppingList.GetAllItems();

            return Ok(items);
        }

        /// <summary>
        /// Add an item to the shopping list
        /// </summary>
        /// <param name="item">The name of the item to add</param>
        /// <returns></returns>
        [HttpPost("{item}")]
        public IActionResult AddItem(string item)
        {
            var result = _shoppingList.TryAddItem(item);

            return GetResponse(result);
        }

        /// <summary>
        /// Toggles the shopping completion status of a given shopping item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{item}")]
        public IActionResult ToggleItemCompletion(string item)
        {
            var result = _shoppingList.TryToggleItemCompletion(item);

            return GetResponse(result);
        }

        /// <summary>
        /// Rename the given shopping item to the given new name
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        [HttpPut("{item}/{newName}")]
        public IActionResult RenameItem(string item, string newName)
        {
            var result = _shoppingList.TryRenameItem(item, newName);

            return GetResponse(result);
        }

        /// <summary>
        /// Remove an item from the shopping list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete("{item}")]
        public IActionResult DeleteItem(string item)
        {
            var result = _shoppingList.TryDeleteItem(item);

            return GetResponse(result);
        }

        /// <summary>
        /// Removes all items from the shopping list
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult RemoveItems()
        {
            _shoppingList.RemoveItems();
            return Ok();
        }

        private IActionResult GetResponse(Models.ActionResult result)
        {
            switch (result.ResultType)
            {
                case ResultType.Conflict:
                    return Conflict(result);
                case ResultType.NotFound:
                    return NotFound();
                case ResultType.Success:
                    return Ok();
                default:
                    return BadRequest(result);
            }
        }
    }
}
