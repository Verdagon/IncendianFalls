using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainEffectObserver {
  void OnTerrainEffect(ITerrainEffect effect);
}

}
