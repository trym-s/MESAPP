namespace Streaming.Settings
{
    public class MqttSettings
    {
        public string Broker { get; set; } = null!;
        public int Port { get; set; }
        public string ClientId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Topic { get; set; } = null!;
    }
}
