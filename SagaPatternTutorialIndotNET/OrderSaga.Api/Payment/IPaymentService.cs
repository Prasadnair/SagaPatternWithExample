using OrderSaga.Api.Models;

namespace OrderSaga.Api.Payment
{
    public class IPaymentService
    {
        public async Task<bool> ProcessPaymentAsync(Order order)
        {
            Console.WriteLine($"Processing payment for order {order.Id}");
            await Task.Delay(100); // Simulating async work
            return true; // Assume success for simplicity
        }

        public async Task UndoProcessPaymentAsync(Order order)
        {
            Console.WriteLine($"Undoing payment for order {order.Id}");
            await Task.Delay(100); // Simulating async work
        }
    }
}
