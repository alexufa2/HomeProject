namespace CompanyContractsWebAPI.Models
{
    public static class ContractStatuses
    {
        public const string New = "New";
        public const string Started = "Started";
        public const string Finished = "Finished";
        public const string Canceled = "Canceled";

        private static Dictionary<string, string> _statusDict = new Dictionary<string, string>
        {
            {New, "Новый" },
            {Started, "Начато исполнение" },
            {Finished, "Завершен" },
            {Canceled, "Отменен" }
        };

        public static string GetStatusName(string status)
        {
            return _statusDict[status];
        }

    }
}
