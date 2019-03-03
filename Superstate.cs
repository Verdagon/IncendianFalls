using System;
using System.Collections.Generic;

namespace Atharia.Model {
  public class Superstate {
    // Views
    public LiveUnitByLocationMap liveUnitByLocationMap;

    // Extra-model state
    public List<RootIncarnation> turnsIncludingPresent;
    public int futuremostTime;

    public Superstate(
        LiveUnitByLocationMap liveUnitByLocationMap,
        List<RootIncarnation> turnsIncludingPresent,
        int futuremostTime) {
      this.liveUnitByLocationMap = liveUnitByLocationMap;
      this.turnsIncludingPresent = turnsIncludingPresent;
      this.futuremostTime = futuremostTime;
    }
  }

}
