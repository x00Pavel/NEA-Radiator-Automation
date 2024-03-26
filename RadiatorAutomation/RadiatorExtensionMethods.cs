using System.Reactive.Linq;
using NetDaemon.HassModel;
using NetDaemon.HassModel.Entities;

namespace NEACustomLib.lib;

public static class RadiatorExtensionMethods
{
    public static void SetTemperature(this IInputNumberEntityCore radiator, double temperature)
    {
        radiator.CallService("input_number.set_value", new { entity_id = new [] { radiator.EntityId }, value = temperature });
    }
    
    public static void SetTemperature(this IEnumerable<IInputNumberEntityCore> radiators, double temperature)
    {
        var inputNumberEntityCores = radiators.ToList();
        var allId = inputNumberEntityCores.Select(r => r.EntityId);
        inputNumberEntityCores.ForEach(radiator => radiator.CallService("input_number.set_value", new { entity_id = allId, value = temperature }));
        
    }

    public static IObservable<StateChange> StateChanged(this IEnumerable<IInputNumberEntityCore> radiators, IHaContext context)
    {
        var inputNumberEntityCores = radiators as IInputNumberEntityCore[] ?? radiators.ToArray();
        return context
            .StateAllChanges()
            .Where(ev => inputNumberEntityCores.Select(e => e.EntityId).Contains(ev.Entity.EntityId));
    }
}