using ATS.Core.MobileCompanies;
using System;
using System.Collections.Generic;
using ATS.Core.ClientsService;
using Logging.Loggers;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);




            
            IMobileCompany mobileCompany = new MobileCompany();

            


            Client user1 = new Client
            {
                FirstName = "A",
                LastName = "AA"
            };
            IClientService clientService1 = new ClientService(mobileCompany.SingClientContract(user1));



            Client user2 = new Client
            {
                FirstName = "B",
                LastName = "BB"
            };
            IClientService clientService2 = new ClientService(mobileCompany.SingClientContract(user2));

            Client user3 = new Client
            {
                FirstName = "C",
                LastName = "CC"
            };
            IClientService clientService3 = new ClientService(mobileCompany.SingClientContract(user3));


            Client user4 = new Client
            {
                FirstName = "D",
                LastName = "DD"
            };
            IClientService clientService4 = new ClientService(mobileCompany.SingClientContract(user4));


            clientService1.ConnectToPort();
            clientService2.ConnectToPort();
            clientService3.ConnectToPort();
            clientService4.ConnectToPort();
            
            clientService1.Call(clientService1.PhoneNumber);

            clientService2.Answer(true);

            clientService2.EndCall();

            clientService1.EndCall();

            clientService2.Call(clientService3.PhoneNumber);

            clientService2.DisconnectFromPort();
            clientService3.Answer(false);



            clientService3.DisconnectFromPort();
            clientService4.Call(clientService3.PhoneNumber);

        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
            Console.WriteLine("Application will be terminated. Press any key...");
            Console.ReadKey();
        }

        
    }
}
