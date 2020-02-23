using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainTileEffectObserver {
  void OnTerrainTileEffect(ITerrainTileEffect effect);
}

}
