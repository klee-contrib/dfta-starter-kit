using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace KleeContrib.Dfta.Migrations.Utils;

/// <summary>
/// Console formatter for logging.
/// </summary>
public class MigrationsConsoleFormatter() : ConsoleFormatter("migrations")
{
    /// <inheritdoc />
    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter
    )
    {
        if (logEntry.Category == "Microsoft.EntityFrameworkCore.Database.Command")
        {
            return;
        }

        if (logEntry.LogLevel > LogLevel.Information)
        {
            textWriter.Write(logEntry.LogLevel == LogLevel.Warning ? "\e[33m" : "\e[1m\e[31m");
            textWriter.Write($"[{logEntry.LogLevel.ToString().ToLower()}] ");
        }

        textWriter.WriteLine(logEntry.Formatter(logEntry.State, logEntry.Exception));

        if (logEntry.LogLevel > LogLevel.Information)
        {
            textWriter.Write("\e[39m\e[22m");
        }
    }
}
