// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names

using HomeAssistantGenerated;
using NetEntityAutomation.Extensions.ExtensionMethods;

namespace HassModel;

/// <summary>
///     Showcase using the new HassModel API and turn on light on movement
/// </summary>
[NetDaemonApp]
public class LightOnMovement
{
    public LightOnMovement(IHaContext ha, SensorEntities sensors, ILogger<LightOnMovement> logger)
    {
        logger.LogDebug(sensors.LivingRoomTemperatureSensorTemperature.State());
        logger.LogDebug(sensors.BedroomTemperatureSensorTemperature.State());
    }
}
