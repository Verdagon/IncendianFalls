using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IITerrainTileComponentMutBunchObserver {
  void OnITerrainTileComponentMutBunchAdd(int id);
  void OnITerrainTileComponentMutBunchRemove(int id);
}

}
