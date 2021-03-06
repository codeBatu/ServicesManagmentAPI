namespace Model
{

    public class ServiceTable
    {
        public ServiceTable()
        {
            LogTables = new HashSet<LogTable>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public ServiceStatusEnum? ServiceStatus { get; set; }
        public string? ActiveLife { get; set; }
        public DateTime? RestDateTime { get; set; }
        public int? RestartCount { get; set; }
        public string? Version { get; set; }
        public bool IsDelete { get; set; }
        public  ICollection<LogTable> LogTables { get; set; }

    }
}
