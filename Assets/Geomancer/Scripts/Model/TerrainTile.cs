using System.Collections.Generic;
using Atharia.Model;

namespace Geomancer.Model {
  public class TerrainTile {
    public readonly Location location;
    public int elevation;
    public List<string> members;
    
    public TerrainTile(
        Location location,
        int elevation,
        List<string> members) {
      this.location = location;
      this.elevation = elevation;
      this.members = members;
    }
    
    public void Destruct() {}
  }
}
