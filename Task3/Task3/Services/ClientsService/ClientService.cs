using System;
using ATS.Core.AutomaticTelephoneSystem;
using ATS.Core.AutomaticTelephoneSystem.Ports;
using ATS.Core.AutomaticTelephoneSystem.Stations;
using ATS.Core.AutomaticTelephoneSystem.Terminals;
using ATS.Core.Clients;
using Logging.Loggers;
using Services.Reports;

namespace Services.ClientsService
{
    public class ClientService : IClientService
    {
        private readonly IClient _client;

        private readonly ITerminal _terminal;

        private readonly IReportService _reportService;

        public ClientService(ITerminal terminal, IReportService reportService, IClient client)
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
            catch (PortException)
            {
                _terminal.Print("Ошибка вызова. Звонок не начался.");
            }
            catch (StationException)
            {
                _terminal.Print("Ошибка вызова. Звонок не начался.");
            }
        }


        public void ConnectToPort()
        {
            try
            {
                _terminal.ConnectToPort();
            }
            catch (PortException)
            {
                _terminal.Print("Ошибка подключения к порту. Порт не подключен.");
            }
            catch (StationException)
            {
                _terminal.Print("Ошибка подключения к порту. Порт не подключен.");
            }
        }


        public void DisconnectFromPort()
        {
            try
            {
                _terminal.DisconnectFromPort();
            }
            catch (PortException)
            {
                _terminal.Print("Ошибка отключения от порта. Порт не отключен.");
            }
            catch (StationException)
            {
                _terminal.Print("Ошибка отключения от порта. Порт не отключен.");
            }
        }


        public void Answer(bool isAccept)
        {
            try
            {
                _terminal.AnswerRequest(isAccept);
            }
            catch (PortException)
            {
                _terminal.Print("Невозможно ответить на звонок.");
            }
            catch (StationException)
            {
                _terminal.Print("Невозможно ответить на звонок.");
            }
        }


        public void EndCall()
        {
            try
            {
                _terminal.End();
            }
            catch (PortException)
            {
                _terminal.Print("Невозможно завершить звонок.");
            }
            catch (StationException)
            {
                _terminal.Print("Невозможно завершить звонок.");
            }
        }

        public void GetReport(DateTime startDate, DateTime endDate, PhoneNumber interlocutorPhoneNumber = null)
        {
            _reportService.CreateReportForClient(_terminal.PhoneNumber, _client.Id, startDate, endDate, interlocutorPhoneNumber: interlocutorPhoneNumber);
        }

        public void GetReport(PhoneNumber interlocutorPhoneNumber = null)
        {
            _reportService.CreateReportForClient(_terminal.PhoneNumber, _client.Id,  interlocutorPhoneNumber: interlocutorPhoneNumber);
        }
    }
}
