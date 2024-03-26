// using NetDaemon.HassModel.Entities;
// using NetEntityAutomation.Core.Automations;
// using NetEntityAutomation.Core.Configs;
// using NetEntityAutomation.Core.Fsm;
//
// namespace NEACustomLib.lib;
//
// public enum SunBlindsState
// {
//     Open,
//     Closed
// }
//
// public enum SunBlindsTrigger
// {
//     SunRise,
//     SunSet,
//     LightIntensityChange
// }
//
// public class SunBlindsFSM: FsmBase<SunBlindsState, SunBlindsTrigger>
// {
//     public SunBlindsFSM(AutomationConfig config, ILogger logger) : base(config, logger)
//     {
//         DefaultState = SunBlindsState.Closed;
//         StoragePath = $"{StorageDir}/sun_blinds_fsm.json";
//         InitFsm();
//     }
//
//     public override void FireAllOff()
//     {
//         throw new NotImplementedException();
//     }
// }
//
// /// <summary>
// /// This automation is responsible for adgusting the blinds based on the sun position and current light intensity.
// /// </summary>
// public class SunBlindsAutomation: AutomationBase<ICoverEntityCore, SunBlindsFSM>
// {
//     public SunBlindsAutomation(IHaContext context, AutomationConfig config, ILogger logger) : base(context, config, logger)
//     {
//     }
//
//     protected override SunBlindsFSM ConfigureFsm(ICoverEntityCore entity)
//     {
//         throw new NotImplementedException();
//     }
// }