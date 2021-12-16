using System;
using System.Collections.Generic;
using Task3.AutomaticTelephoneSystem;
using Task3.BillingSystem;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IMobileCompany mobileCompany = new MobileCompany();


            User user1 = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov"
            };
            UserService userService1 = new UserService(user1,
                mobileCompany.MakeUserContract(user1));



            User user2 = new User
            {
                FirstName = "Petr",
                LastName = "Petrov"
            };
            UserService userService2 = new UserService(user2,
                mobileCompany.MakeUserContract(user2));

            User user3 = new User
            {
                FirstName = "Dmitry",
                LastName = "Dmitriev"
            };
            UserService userService3 = new UserService(user3,
                mobileCompany.MakeUserContract(user3));


            //Terminal terminal1 = new Terminal();
            //PhoneNumber ph1 = terminal1.PhoneNumber;

            //Terminal terminal2 = new Terminal();
            //PhoneNumber ph2 = terminal1.PhoneNumber;

            //Terminal terminal3 = new Terminal();
            //PhoneNumber ph3 = terminal1.PhoneNumber;

            //Port port1 = new Port(1);
            //Port port2 = new Port(2);
            //Port port3 = new Port(3);


            //PortController portController = new PortController(new List<Port>(new Port[] { port1, port2, port3 }));



            //Station station = new Station(contracts);

            //terminal1.StartCall += port1.OnCall;
            //terminal2.StartCall += port2.OnCall;

            //port1.StartCall += station.OnCall;
            //port2.StartCall += station.OnCall;

            //port1.Request += terminal1.OnRequest;
            //port2.Request += terminal2.OnRequest;

            //port2.PortState = States.PortState.Disconnected;

            try
            {
                //terminal1.Call(terminal2.PhoneNumber);
                userService1.Call(userService2.PhoneNumber);
            }
            catch (PortException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationException ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                userService2.Call(userService3.PhoneNumber);
            }
            catch (PortException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (StationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                userService3.Call(userService2.PhoneNumber);
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
