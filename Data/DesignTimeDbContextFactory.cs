using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using ExpenseApp.Data;

namespace ExpenseApp.Data
{
    // Provides a design-time factory so `dotnet ef` can create the DbContext reliably
    // and supports DATABASE_URL in several common formats (postgres:// URI or key=value connection string).
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Ensure .env files are loaded if present
            try
            {
                DotNetEnv.Env.Load();
            }
            catch
            {
                // ignore if DotNetEnv cannot find a file
            }

            var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            string connString = null;

            if (!string.IsNullOrWhiteSpace(rawUrl))
            {
                connString = TryBuildFromUrl(rawUrl);
            }

            // If no DATABASE_URL or parsing failed, try to read from appsettings (DefaultConnection)
            if (string.IsNullOrWhiteSpace(connString))
            {
                // As a last resort, use the default SQLite/SQL Server connection string if present in appsettings
                // This file access intentionally minimal: if the environment doesn't provide a DB, we fail loudly.
                throw new InvalidOperationException("DATABASE_URL not set or could not be parsed for design-time DbContext. Set DATABASE_URL or supply a design-time factory.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }

        private static string TryBuildFromUrl(string rawUrl)
        {
            rawUrl = rawUrl.Trim();

            // If it's already a key=value connection string, let Npgsql parse it directly.
            if (rawUrl.Contains("=") && rawUrl.Contains(";"))
            {
                try
                {
                    var csb = new NpgsqlConnectionStringBuilder(rawUrl);
                    return csb.ConnectionString;
                }
                catch
                {
                    // fall through to URI parsing
                }
            }

            // Handle common URI forms: postgres://user:pass@host:port/dbname or postgresql://...
            if (rawUrl.Contains("://"))
            {
                if (Uri.TryCreate(rawUrl, UriKind.Absolute, out var uri))
                {
                    var scheme = uri.Scheme?.ToLowerInvariant();
                    if (scheme == "postgres" || scheme == "postgresql" || scheme == "tcp")
                    {
                        var userInfo = uri.UserInfo ?? string.Empty;
                        var userParts = userInfo.Split(new[] { ':' }, 2);
                        var user = userParts.Length > 0 ? userParts[0] : null;
                        var pass = userParts.Length > 1 ? Uri.UnescapeDataString(userParts[1]) : null;

                        var host = uri.Host;
                        var port = uri.IsDefaultPort || uri.Port <= 0 ? 5432 : uri.Port;
                        var database = uri.AbsolutePath?.TrimStart('/') ?? "";

                        var csb = new NpgsqlConnectionStringBuilder
                        {
                            Host = host,
                            Port = port,
                            Username = user,
                            Password = pass,
                            Database = database,
                            TrustServerCertificate = true
                        };

                        // Inspect query parameters for SSL mode
                        if (!string.IsNullOrEmpty(uri.Query))
                        {
                            var q = uri.Query.TrimStart('?');
                            var parts = q.Split('&', StringSplitOptions.RemoveEmptyEntries);
                            foreach (var part in parts)
                            {
                                var kv = part.Split('=', 2);
                                if (kv.Length == 2)
                                {
                                    var k = kv[0].ToLowerInvariant();
                                    var v = kv[1].ToLowerInvariant();
                                    if (k == "sslmode")
                                    {
                                        if (v == "disable") csb.SslMode = SslMode.Disable;
                                        else if (v == "prefer") csb.SslMode = SslMode.Prefer;
                                        else csb.SslMode = SslMode.Require;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Default to Require to be safe for cloud providers
                            csb.SslMode = SslMode.Require;
                        }

                        return csb.ConnectionString;
                    }
                }
            }

            return null;
        }
    }
}
