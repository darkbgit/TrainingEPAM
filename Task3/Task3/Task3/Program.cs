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


            IMobileCompany mobileCompany = new MobileCompany();

            ILogger logger = new ConsoleLogger();


            User user1 = new User
            {
                FirstName = "A",
                LastName = "AA"
            };
            UserService userService1 = new UserService(user1,
                mobileCompany.MakeUserContract(user1), logger);



            User user2 = new User
            {
                FirstName = "B",
                LastName = "BB"
            };
            UserService userService2 = new UserService(user2,
                mobileCompany.MakeUserContract(user2), logger);

            User user3 = new User
            {
                FirstName = "C",
                LastName = "CC"
            };
            UserService userService3 = new UserService(user3,
                mobileCompany.MakeUserContract(user3), logger);


            User user4 = new User
            {
                FirstName = "D",
                LastName = "DD"
            };
            UserService userService4 = new UserService(user4,
                mobileCompany.MakeUserContract(user4), logger);


            userService1.ConnectToPort();
            userService2.ConnectToPort();
            userService3.ConnectToPort();
            
            userService1.Call(userService2.PhoneNumber);

            userService2.Answer(true);

            userService2.Call(userService3.PhoneNumber);

            userService3.Call(userService2.PhoneNumber);

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
