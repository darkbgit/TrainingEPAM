using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace CsvManager
{
    internal class SingleGlobal : IDisposable
    {
        private static Semaphore _semaphore;

        public SingleGlobal(int timeOut)
        {
            InitSemaphore();
            try
            {
                IsSingle = _semaphore.WaitOne(timeOut < 0 ? Timeout.Infinite : timeOut);
            }
            catch (AbandonedMutexException)
            {
                IsSingle = true;
            }
        }

        public bool IsSingle { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static void InitSemaphore()
        {
            const string APP_ID = @"Global\27A9F7D0-F215-4E1E-840C-7B0F4267D8D6";

            _semaphore = new Semaphore(1, 1, APP_ID);

            var allowEveryoneRule = new SemaphoreAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                SemaphoreRights.FullControl, AccessControlType.Allow);

            var securitySettings = new SemaphoreSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            _semaphore.SetAccessControl(securitySettings);
        }

        public void Dispose()
        {
            if (_semaphore == null) return;

            if (IsSingle)
            {
                _semaphore.Release();
            }
            _semaphore.Close();
        }
    }
}
