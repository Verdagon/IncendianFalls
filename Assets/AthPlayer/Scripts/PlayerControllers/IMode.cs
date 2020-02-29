using System;
using Atharia.Model;

namespace AthPlayer {
  public interface IModeDelegate {
    void AfterDidSomething();
    void SwitchToNormalMode();
    void SwitchToTimeAnchorMoveMode();
    void SwitchToFireMode();
  }

  public interface IMode {
    void OnTileMouseClick(Location newLocation);

    void DefendClicked();
    void FireClicked();
    void CounterClicked();
    void InteractClicked();  

    void TimeShiftClicked();
    void TimeAnchorMoveClicked();
    void ActivateCheat(string cheatName);

    void ReadyForTurn();
  }
}
