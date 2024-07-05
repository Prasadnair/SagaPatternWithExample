using OrderSaga.Api.Models;

namespace OrderSaga.Api.Inventory
{
    public class InventoryService
    {
        public async Task<bool> ReserveInventoryAsync(Order order)
        {
            Console.WriteLine($"Reserving inventory for order {order.Id}");
            await Task.Delay(100); // Simulating async work
            return true; // Assume success for simplicity
        }

        public async Task UndoReserveInventoryAsync(Order order)
        {
            Console.WriteLine($"Undoing inventory reservation for order {order.Id}");
            await Task.Delay(100); // Simulating async work
        }
    }
}
