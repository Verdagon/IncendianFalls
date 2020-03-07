using System;
using Atharia.Model;

namespace AthPlayer {
  public interface IModeDelegate {
    void AfterDidSomething();
    void SwitchToNormalMode();
    void SwitchToTimeAnchorMoveMode();
    void SwitchToFireMode();
    void SwitchToMireMode();
  }

  public interface IMode {
    void OnTileMouseClick(Location newLocation);

    void DefyClicked();
    void FireClicked();
    void MireClicked();
    void CounterClicked();
    void InteractClicked();  

    void TimeShiftClicked();
    void TimeAnchorMoveClicked();
    void ActivateCheat(string cheatName);

    void ReadyForTurn();
  }
}
