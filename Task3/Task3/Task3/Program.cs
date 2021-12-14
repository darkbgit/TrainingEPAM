using System;
using Task3.AutomaticTelephoneSystem;
using Task3.BillingSystem;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ats ats = new Ats();


            User user1 = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov"
            };
            UserService userService1 = new UserService(user1, ats.MakeUserContract(user1));

            PortListener portListener = new PortListener(userService1.Contract.Port, ats.Billing);

            User user2 = new User
            {
                FirstName = "Petr",
                LastName = "Petrov"
            };
            UserService userService2 = new UserService(user2, ats.MakeUserContract(user2));

            User user3 = new User
            {
                FirstName = "Dmitry",
                LastName = "Dmitriev"
            };
            UserService userService3 = new UserService(user3, ats.MakeUserContract(user3));



            //userService1.ConnectToPort();
            //userService1.CallTo(user2);
            //userService1.EndCall();
            //userService1.DisconnectFromPort();



            Terminal terminal1 = new Terminal();
            Terminal terminal2 = new Terminal();
            Terminal terminal3 = new Terminal();

            Port port1 = new Port(1);
            Port port2 = new Port(2);
            Port port3 = new Port(3);

            Station station = new Station();

            terminal1.StartCalling += port1.OnCall;
            terminal2.StartCalling += port2.OnCall;

            port1.Calling += station.OnCall;
            port2.Calling += station.OnCall;

            station.SendRequest += port1.OnRequest;
            station.SendRequest += port2.OnRequest;

            port1.SendRequest += terminal1.OnRequest;
            port2.SendRequest += terminal2.OnRequest;

            //port2.PortState = States.PortState.Disconnected;

            try
            {
                terminal1.StartCall(terminal2.PhoneNumber);
            }
            catch (PortException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
