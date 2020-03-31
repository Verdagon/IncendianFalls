using System;
using Atharia.Model;

namespace AthPlayer {
  public interface IModeDelegate {
    void SwitchToNormalMode();
  }

  public interface IMode {
    void OnTileMouseClick(Location newLocation);

    void Update(Location maybeHoverLocation);

    // purposeful true means they explicitly hit cancel. false means they did
    // something unexpected, and we should tell them that they did the thing
    // wrong.
    void Destroy(bool purposeful);
  }
}
