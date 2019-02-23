using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEffectObserver {
  void OnTerrainTileEffect(ITerrainTileEffect effect);
}

}
