using ATS.Core.MobileCompanies;
using System;
using System.Collections.Generic;
using System.Threading;
using ATS.Core.BillingSystem;
using ATS.Core.ClientsService;
using ATS.Core.Reports;
using ATS.Core.Tariffs;
using Logging.Loggers;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            ITariff tariff = new Tariff();

            //Contracts contracts = new Contracts();

            //IBilling billing = new Billing();
            
            IMobileCompany mobileCompany = new MobileCompany(new List<ITariff>{tariff});

            IReportService reportService = new ReportService(mobileCompany.Billing);


            Client client1 = new Client
            {
                FirstName = "A",
                LastName = "AA"
            };
            IClientService clientService1 = 
                new ClientService(mobileCompany.SingClientContract(client1, tariff),
                reportService, client1);



            Client client2 = new Client
            {
                FirstName = "B",
                LastName = "BB"
            };
            IClientService clientService2 = 
                new ClientService(mobileCompany.SingClientContract(client2, tariff),
                    reportService, client2);

            Client client3 = new Client
            {
                FirstName = "C",
                LastName = "CC"
            };
            IClientService clientService3 = 
                new ClientService(mobileCompany.SingClientContract(client3, tariff),
                    reportService, client3);


            Client client4 = new Client
            {
                FirstName = "D",
                LastName = "DD"
            };
            IClientService clientService4 =
                new ClientService(mobileCompany.SingClientContract(client4, tariff),
                    reportService, client4);

            var dt1 = DateTime.Now.ToUniversalTime();

            clientService1.ConnectToPort();
            clientService2.ConnectToPort();
            clientService3.ConnectToPort();
            clientService4.ConnectToPort();
            
            clientService1.Call(clientService2.PhoneNumber);

            clientService2.Answer(true);

            Thread.Sleep(2000);

            clientService2.EndCall();

            clientService1.EndCall();

            clientService2.Call(clientService3.PhoneNumber);

            clientService2.DisconnectFromPort();
            clientService3.Answer(false);



            clientService3.DisconnectFromPort();
            clientService4.Call(clientService3.PhoneNumber);

            clientService1.GetReport(dt1, DateTime.Now.ToUniversalTime());
            clientService2.GetReport(dt1, DateTime.Now.ToUniversalTime());
            clientService3.GetReport(dt1, DateTime.Now.ToUniversalTime());

        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.LogMessage("Error " + (e.ExceptionObject as Exception)?.Message);
            Log.LogMessage("Application will be terminated. Press any key...");
        }

        
    }
}
