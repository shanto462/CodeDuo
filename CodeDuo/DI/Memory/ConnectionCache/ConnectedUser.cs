namespace CodeDuo.DI.Memory.ConnectionCache
{
    public class ConnectedUser
    {
        public string UserId { get; set; }
        public string Guid { get; set; }
        public string ConnectionKey { get; set; }
        public DateTime ConnectionTime { get; set; }
    }
}
