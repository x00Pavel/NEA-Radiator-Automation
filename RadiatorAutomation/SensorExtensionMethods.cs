using System.Reactive.Linq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;

namespace RadiatorAutomation;

public static class SensorExtensionMethods
{
    public static IObservable<StateChange> StateChanged(this IEnumerable<ISensorEntityCore> radiators, IHaContext context)
    {
        var inputNumberEntityCores = radiators as ISensorEntityCore[] ?? radiators.ToArray();
        return context
            .StateAllChanges()
            .Where(ev => inputNumberEntityCores.Select(e => e.EntityId).Contains(ev.Entity.EntityId));
    }
}