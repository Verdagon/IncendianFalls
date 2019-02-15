using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEffectObserver {
  void OnTerrainTileEffect(ITerrainTileEffect effect);
}

}
