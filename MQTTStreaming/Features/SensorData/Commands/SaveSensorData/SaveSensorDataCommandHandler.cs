using MediatR;
using Microsoft.EntityFrameworkCore;
using MQTTStreaming.Database;
using Shared.Entities;

namespace MQTTStreaming.Features.SensorData.Commands.SaveSensorData;

public class SaveSensorDataCommandHandler(MqttStreamingDbContext context) : IRequestHandler<SaveSensorDataCommand, Unit>
{
    public async Task<Unit> Handle(SaveSensorDataCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var entries = new List<Shared.Entities.SensorData>();

        // Aktif workorderId'yi bul
        var activeWorkorderId = await context.Workorders
            .Where(w => w.WorkstationId == payload.WorkstationId && w.IsActive)
            .Select(w => (int?)w.WorkorderId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (activeWorkorderId == null)
            Console.WriteLine($"[WARN] Aktif Workorder bulunamadı! WorkstationId: {payload.WorkstationId}");

        foreach (var m in payload.Measurements)
        {
            int? sensorTypeId = m.SensorTypeId;

            // SensorName üzerinden eşleşme yapılacaksa
            if (sensorTypeId == null && !string.IsNullOrWhiteSpace(m.SensorName))
            {
                sensorTypeId = await context.SensorTypes
                    .Where(x => x.Name == m.SensorName)
                    .Select(x => (int?)x.Id)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            if (sensorTypeId == null)
                throw new Exception($"SensorType bulunamadı: {m.SensorName ?? "null"}");

            // Log tablosuna her veriyi ekle
            entries.Add(new Shared.Entities.SensorData
            {
                Id = Guid.NewGuid(),
                WorkstationId = payload.WorkstationId,
                WorkorderId = activeWorkorderId,
                SensorTypeId = sensorTypeId.Value,
                SensorValue = m.Value,
                Timestamp = payload.Timestamp
            });
        }

        if (entries.Any())
        {
            await context.SensorData.AddRangeAsync(entries, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
