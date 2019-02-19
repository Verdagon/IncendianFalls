using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class TerrainTile {
  public readonly Root root;
  public readonly int id;
  public TerrainTile(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TerrainTileIncarnation incarnation { get { return root.GetTerrainTileIncarnation(id); } }
  public void AddObserver(ITerrainTileEffectObserver observer) {
    root.AddTerrainTileObserver(id, observer);
  }
  public void RemoveObserver(ITerrainTileEffectObserver observer) {
    root.RemoveTerrainTileObserver(id, observer);
  }
  public void Delete() {
    root.EffectTerrainTileDelete(id);
  }
  public static TerrainTile Null = new TerrainTile(null, 0);
  public bool Exists() { return root != null && root.TerrainTileExists(id); }
  public bool NullableIs(TerrainTile that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(TerrainTile that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int elevation {
    get { return incarnation.elevation; }
    set { root.EffectTerrainTileSetElevation(id, value); }
  }
  public bool walkable {
    get { return incarnation.walkable; }
  }
  public string classId {
    get { return incarnation.classId; }
  }
  public IFeatureMutList features {
    get { return new IFeatureMutList(root, incarnation.features); }
  }
}
}
