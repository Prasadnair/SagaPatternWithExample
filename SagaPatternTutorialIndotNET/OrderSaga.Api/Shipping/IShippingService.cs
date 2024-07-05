using OrderSaga.Api.Models;

namespace OrderSaga.Api.Shipping
{
    public class IShippingService
    {
        public async Task<bool> ShipOrderAsync(Order order)
        {
            Console.WriteLine($"Shipping order {order.Id}");
            await Task.Delay(100); // Simulating async work
            return true; // Assume success for simplicity
        }

        public async Task UndoShipOrderAsync(Order order)
        {
            Console.WriteLine($"Undoing shipping for order {order.Id}");
            await Task.Delay(100); // Simulating async work
        }
    }
}
