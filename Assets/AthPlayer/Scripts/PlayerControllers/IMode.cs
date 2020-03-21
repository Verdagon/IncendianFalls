using System;
using Atharia.Model;

namespace AthPlayer {
  public interface IModeDelegate {
    void AfterDidSomething();
    void SwitchToCapability(int capabilityId);
    void SwitchToNormalMode();
  }

  public interface IMode {
    void OnTileMouseClick(Location newLocation);
    // purposeful true means they explicitly hit cancel. false means they did
    // something unexpected, and we should tell them that they did the thing
    // wrong.
    void Cancel(bool purposeful);
  }
}
