using MediatR;
using MQTTStreaming.Features.SensorData.DTOs;

namespace MQTTStreaming.Features.SensorData.Commands.SaveSensorData;

public class SaveSensorDataCommand : IRequest<Unit> 
{
    public SensorDataPayloadDto Payload { get; set; }

    public SaveSensorDataCommand(SensorDataPayloadDto payload)
    {
        Payload = payload;
    }
}
