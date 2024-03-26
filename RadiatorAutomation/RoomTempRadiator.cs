using System.Globalization;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using NEACustomLib.lib;
using NetDaemon.HassModel.Entities;
using NetEntityAutomation.Core.Automations;

namespace RadiatorAutomation;

public class RoomTempRadiator: AutomationBase<ISensorEntityCore>
{
    private List<ISensorEntityCore> Sensors { get; init; }
    private List<IInputNumberEntityCore> Radiators { get; set; }
    public required double MaxTemp { get; set; }
    public required double MinTemp { get; set; }
    public double HittingTemp { get; set; } = 23;
    public double CoolingTemp { get; set; } = 20;
    
    public RoomTempRadiator(IEnumerable<ISensorEntityCore> sensors, IEnumerable<IInputNumberEntityCore> radiators, ILogger logger) : base()
    {
        Radiators = radiators.ToList();
        Sensors = sensors.ToList();
        Logger = logger;
        Logger.LogDebug("RoomTempRadiator created");
    }
    
    public override void ConfigureAutomation()
    {
        
        HighTempEvent().Subscribe(e =>
        {
            // var old = double.Parse(e.Old?.State!);
            // var current = double.Parse(e.New?.State!);
            //
            // if (old < current || current < MaxTemp || old > current) return;
            Logger.LogDebug("High temp event fired");
            Radiators.SetTemperature(HittingTemp);
        });
        LowTempEvent().Subscribe(_ =>
        {
            Logger.LogDebug("High temp event fired");
            Radiators.SetTemperature(CoolingTemp);
        });
    }

    private IObservable<StateChange> SensorEvent() {
        return Sensors.StateChanged(Radiators.First().HaContext).Where(e =>
        {   
            Logger.LogDebug("Sensor event fired: {State}", e.New?.State ?? "null");
            switch (e.New?.State)
            {
                case null:
                case "unavailable":
                    return false;
                default:
                    return true;
            }
        });
    }
    
    private IObservable<StateChange> HighTempEvent() => SensorEvent()
        .Where(e =>
        {
            var res = double.Parse(e.New?.State!, CultureInfo.InvariantCulture) > MaxTemp;
            Logger.LogDebug("High temp event fired: {Res}", res);
            return res;
        });
    
    private IObservable<StateChange> LowTempEvent() => SensorEvent()
        .Where(e =>
        {
            var res =double.Parse(e.New?.State!, CultureInfo.InvariantCulture) < MinTemp;
            Logger.LogDebug("High temp event fired: {Res}", res);
            return res;
        });
    
}