using System;
using Atharia.Model;

namespace AthPlayer {
  public interface IModeDelegate {
    void AfterDidSomething();
    void SwitchToNormalMode();
    void SwitchToTimeAnchorMoveMode();
    void SwitchToFireMode();
    void SwitchToFireBombMode();
    void SwitchToMireMode();
  }

  public interface IMode {
    void OnTileMouseClick(Location newLocation);

    void DefyClicked();
    void FireClicked();
    void FireBombClicked();
    void MireClicked();
    void CancelClicked();
    void CounterClicked();
    void InteractClicked();  

    void TimeShiftClicked();
    void TimeAnchorMoveClicked();
    void ActivateCheat(string cheatName);

    void ReadyForTurn();
  }
}
