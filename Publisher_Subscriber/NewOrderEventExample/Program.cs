using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewOrderEventExample
{
    public class OrderEventArgs : EventArgs
    {
        public int OrderId { get;}
        public int OrderTotalPrice { get; }
        public string ClientEmail { get; }

        public OrderEventArgs(int orderID, int orderTotalPrice, string clientEmail)
        {
            this.OrderId = orderID;
            this.OrderTotalPrice = orderTotalPrice;
            this.ClientEmail = clientEmail;
        }
    }
    public class Order
    {
        public event EventHandler<OrderEventArgs> OnOrderCreated;
        public void Create(int orderID, int orderTotalPrice, string clientEmail)
        {
            Console.WriteLine("New Order created; now will notify everyone by raising the event.\n");
            if (OnOrderCreated != null)
            {
                OnOrderCreated(this, new OrderEventArgs(orderID, orderTotalPrice, clientEmail));
            }
        }
    }
    public class EmailService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void Unsubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(object sender, OrderEventArgs e)
        {
            Console.WriteLine("--------Email Service--------");
            Console.WriteLine($"SMS Service Object Recieved a new order event");
            Console.WriteLine($"Order ID: {e.OrderId}");
            Console.WriteLine($"Order Price: {e.OrderTotalPrice}");
            Console.WriteLine($"Client Email: {e.ClientEmail}");
            Console.WriteLine($"\nSend an email");
            Console.WriteLine("-----------------------------");
            /*
             Here is the Code to send Email to the client
            */
            Console.WriteLine();

        }
    }
    public class SMSService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void UnSubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(object sender, OrderEventArgs e)
        {
            Console.WriteLine("--------SMS Service--------");
            Console.WriteLine($"SMS Service Object Recieved a new order event");
            Console.WriteLine($"Order ID: {e.OrderId}");
            Console.WriteLine($"Order Price: {e.OrderTotalPrice}");
            Console.WriteLine($"Client Email: {e.ClientEmail}");
            Console.WriteLine($"\nSend an SMS");
            Console.WriteLine("-----------------------------");
            /*
             Here is the Code to send SMS to the client
            */
            Console.WriteLine();
        }
    }
    public class ShippingService
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandleNewOrder;
        }
        public void UnSubscribe(Order order)
        {
            order.OnOrderCreated -= HandleNewOrder;
        }
        public void HandleNewOrder(object sender, OrderEventArgs e)
        {
            Console.WriteLine("--------Shipping Service--------");
            Console.WriteLine($"Shipping Service Object Recieved a new order event");
            Console.WriteLine($"Order ID: {e.OrderId}");
            Console.WriteLine($"Order Price: {e.OrderTotalPrice}");
            Console.WriteLine($"Client Email: {e.ClientEmail}");
            Console.WriteLine($"\nSend an SMS");
            Console.WriteLine("-----------------------------");
            /*
             Here is the Code to Handle Shipping to the client
            */
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new Order();
            
            EmailService emailService = new EmailService();
            SMSService smsService = new SMSService();
            ShippingService shippingService = new ShippingService();
            
            emailService.Subscribe(order);
            smsService.Subscribe(order);
            shippingService.Subscribe(order);
            
            order.Create(10,540,"Ahmed@gmail.com");

            Console.ReadLine();
        }
    }
}
