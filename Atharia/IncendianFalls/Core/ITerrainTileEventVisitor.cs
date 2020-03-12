using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEventVisitor {
  void Visit(UnitUnleashBideEventAsITerrainTileEvent obj);
}

}
