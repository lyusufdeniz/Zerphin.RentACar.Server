﻿namespace Zerphin.RentACar.Server.Domain.Options
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
