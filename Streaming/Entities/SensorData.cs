namespace Streaming.Entities
{
    public class SensorData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int WorkstationId { get; set; }
        public int? WorkorderId { get; set; } // O anda aktif workorder varsa
        public int SensorTypeId { get; set; } // MQTT'den gelen typeId (0: Temp, 1: Vibration, vs.)
        public double SensorValue { get; set; } // Hem gerçek sıcaklık/titreşim değerleri hem de 0-1 gibi kategorik anlamlar burada tutulacak
        public DateTime Timestamp { get; set; }
    }
}