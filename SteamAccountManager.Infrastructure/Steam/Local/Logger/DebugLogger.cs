﻿using SteamAccountManager.Domain.Steam.Local.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Logger
{
    public class DebugLogger : ILogger
    {
        public void LogDebug(string message, [CallerMemberName] string callerMemberName = "")
        {
            Debug.WriteLine($"[DEBUG] [{callerMemberName}] {message}");
        }

        public void LogException(string message, Exception exception, [CallerMemberName] string callerMemberName = "")
        {
            Debug.WriteLine($"[EXCEPTION] [{callerMemberName}] {message} {exception}");
        }

        public void LogInformation(string message, [CallerMemberName] string callerMemberName = "")
        {
            Debug.WriteLine($"[INFORMATION] [{callerMemberName}] {message}");
        }

        public void LogWarning(string message, Exception exception = null, [CallerMemberName] string callerMemberName = "")
        {
            Debug.WriteLine($"[WARNING] [{callerMemberName}] {message} {exception}");
        }
    }
}