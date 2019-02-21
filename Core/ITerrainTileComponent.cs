using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileComponent {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(ITerrainTileComponent that);
  bool NullableIs(ITerrainTileComponent that);
       }
}
