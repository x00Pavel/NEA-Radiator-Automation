using System.Collections.Generic;
using HomeAssistantGenerated;
using NetEntityAutomation.Core.Automations;
using NetEntityAutomation.Core.Configs;
using NetEntityAutomation.Core.RoomManager;
using RadiatorAutomation;

namespace NEACustomLib.apps.NEA;

public class RadiatorConfig(
    SensorEntities sensors,
    InputNumberEntities inputs,
    ILogger<RadiatorConfig> logger,
    PersonEntities personEntities)
    : IRoomConfig
{
    public ILogger Logger { get; set; } = logger;
    public IEnumerable<AutomationBase> Entities { get; set; } = new List<AutomationBase>
    {
        new RoomTempRadiator(
            new []{sensors.TemperatureSensor},
            new []{inputs.TmpRadiator, inputs.TmpRadiator2},
            logger
        )
        {
            MaxTemp = 30,
            MinTemp = 20,
            HittingTemp = 24,
            CoolingTemp = 20,
            ServiceAccountId = personEntities.Netdaemon.Attributes?.UserId ?? "",
        }
    };

    public NightModeConfig? NightMode { get; set; }
}