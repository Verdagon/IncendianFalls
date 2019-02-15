using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainEffectObserver {
  void OnTerrainEffect(ITerrainEffect effect);
}

}
