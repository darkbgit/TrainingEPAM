using System;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.BillingSystem;
using ATS.Core.Reports;
using Logging.Loggers;


namespace ATS.Core.ClientsService
{
    public class ClientService : IClientService
    {
        private readonly Client _client;

        private readonly ITerminal _terminal;

        private readonly IReportService _reportService;

        public ClientService(ITerminal terminal, IReportService reportService, Client client)
        {
            _terminal = terminal;
            _reportService = reportService;
            _client = client;
        }

        public PhoneNumber PhoneNumber => _terminal.PhoneNumber;


        public void Call(PhoneNumber targetNumber)
        {
            try
            {
                _terminal.Call(targetNumber);
            }
            catch (PortException ex)
            {
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }


        public void ConnectToPort()
        {
            try
            {
                _terminal.ConnectToPort();
            }
            catch (PortException ex)
            {
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }


        public void DisconnectFromPort()
        {
            try
            {
                _terminal.DisconnectFromPort();
            }
            catch (PortException ex)
            {
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }


        public void Answer(bool isAccept)
        {
            try
            {
                _terminal.AnswerRequest(isAccept);
            }
            catch (PortException ex)
            {
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }


        public void EndCall()
        {
            try
            {
                _terminal.End();
            }
            catch (PortException ex)
            {
                Log.LogMessage(ex.Message);
            }
            catch (StationException ex)
            {
                Log.LogMessage(ex.Message);
            }
        }

        public void GetReport(DateTime startDate, DateTime endDate)
        {
            _reportService.CreateReportForClient(startDate, endDate, _client.Id, _terminal.PhoneNumber);
        }
    }
}
