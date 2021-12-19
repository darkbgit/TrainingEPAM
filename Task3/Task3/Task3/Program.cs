using System;
using System.Collections.Generic;
using Task3.AutomaticTelephoneSystem;
using Task3.BillingSystem;
using Task3.Loggers;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);



            ILogger logger = new ConsoleLogger();
            
            IMobileCompany mobileCompany = new MobileCompany(logger);

            


            Client user1 = new Client
            {
                FirstName = "A",
                LastName = "AA"
            };
            ClintService userService1 = new ClintService(mobileCompany.MakeUserContract(user1), logger);



            Client user2 = new Client
            {
                FirstName = "B",
                LastName = "BB"
            };
            ClintService userService2 = new ClintService(mobileCompany.MakeUserContract(user2), logger);

            Client user3 = new Client
            {
                FirstName = "C",
                LastName = "CC"
            };
            ClintService userService3 = new ClintService(mobileCompany.MakeUserContract(user3), logger);


            Client user4 = new Client
            {
                FirstName = "D",
                LastName = "DD"
            };
            ClintService userService4 = new ClintService(mobileCompany.MakeUserContract(user4), logger);


            userService1.ConnectToPort();
            userService2.ConnectToPort();
            userService3.ConnectToPort();
            userService4.ConnectToPort();
            
            userService1.Call(userService2.PhoneNumber);

            userService2.Answer(true);

            userService1.EndCall();

            userService2.EndCall();

            userService3.Call(userService2.PhoneNumber);

            userService3.DisconnectFromPort();
            userService2.Answer(false);



            userService3.DisconnectFromPort();
            userService4.Call(userService3.PhoneNumber);

        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }

        
    }
}
