using OrderSaga.Api.Inventory;
using OrderSaga.Api.Models;
using OrderSaga.Api.Payment;
using OrderSaga.Api.Shipping;

namespace OrderSaga.Api.Saga
{
    public class OrderSagaService
    {
        private readonly InventoryService _inventoryService;
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;

        public OrderSagaService(InventoryService inventoryService,
                         IPaymentService paymentService,
                         IShippingService shippingService)
        {
            _inventoryService = inventoryService;
            _paymentService = paymentService;
            _shippingService = shippingService;
        }

        public async Task<bool> ProcessOrderAsync(Order order)
        {
            try
            {
                bool inventoryReserved = await _inventoryService.ReserveInventoryAsync(order);
                if (!inventoryReserved) throw new Exception("Failed to reserve inventory");

                bool paymentProcessed = await _paymentService.ProcessPaymentAsync(order);
                if (!paymentProcessed) throw new Exception("Failed to process payment");

                bool orderShipped = await _shippingService.ShipOrderAsync(order);
                if (!orderShipped) throw new Exception("Failed to ship order");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await CompensateAsync(order);
                return false;
            }
        }

        private async Task CompensateAsync(Order order)
        {
            await _shippingService.UndoShipOrderAsync(order);
            await _paymentService.UndoProcessPaymentAsync(order);
            await _inventoryService.UndoReserveInventoryAsync(order);
        }
    }
}
