using ATS.Core.MobileCompanies;
using ATS.Core.Tariffs;
using Logging.Loggers;
using System;
using System.Linq;
using System.Threading;
using ATS.Core.Clients;
using Services.ClientsService;
using Services.Reports;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            IMobileCompany mobileCompany = new MobileCompany();

            mobileCompany.AddTariff(new Tariff());

            IReportService reportService = new ReportService(mobileCompany.Billing);

            IClient client1 = new Client()
            {
                FirstName = "A",
                LastName = "AA"
            };
            IClientService clientService1 =
                new ClientService(mobileCompany.SingClientContract(client1,
                        mobileCompany.Tariffs.First()),
                    reportService,
                    client1);

            Client client2 = new()
            {
                FirstName = "B",
                LastName = "BB"
            };
            IClientService clientService2 =
                new ClientService(mobileCompany.SingClientContract(client2, mobileCompany.Tariffs.First()),
                    reportService, client2);

            Client client3 = new()
            {
                FirstName = "C",
                LastName = "CC"
            };
            IClientService clientService3 =
                new ClientService(mobileCompany.SingClientContract(client3, mobileCompany.Tariffs.First()),
                    reportService, client3);

            Client client4 = new()
            {
                FirstName = "D",
                LastName = "DD"
            };
            IClientService clientService4 =
                new ClientService(mobileCompany.SingClientContract(client4, mobileCompany.Tariffs.First()),
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
            clientService3.Answer(true);
            clientService3.EndCall();


            clientService3.DisconnectFromPort();
            clientService4.Call(clientService3.PhoneNumber);

            clientService1.GetReport(dt1, DateTime.Now.ToUniversalTime());
            clientService2.GetReport();
            clientService2.GetReport(clientService3.PhoneNumber);
            clientService3.GetReport(dt1, DateTime.Now.ToUniversalTime());

            //mobileCompany.CancelClientContract(client3);

            //clientService2.GetReport();

        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.LogMessage("Error " + (e.ExceptionObject as Exception)?.Message);
            Log.LogMessage("Application will be terminated. Press any key...");
        }


    }
}
