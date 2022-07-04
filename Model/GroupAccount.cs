namespace Model
{
    public partial class GroupAccount
    {

        public bool? CanUpdate { get; set; } = false;
        public bool? CanCreate { get; set; } = false;
        public bool? CanRemove { get; set; } = false;
        public bool? CanGetAll { get; set; } = false;
        public int AccountId { get; set; } 
        public bool? CanActive { get; set; } = false;
        public bool? CanInActive { get; set; } = false;
        public bool? CanRestart { get; set; } = false;

        public Account Account { get; set; }
    }
}
