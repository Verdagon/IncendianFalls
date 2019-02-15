using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Terrain {
  public readonly Root root;
  public readonly int id;
  public Terrain(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TerrainIncarnation incarnation { get { return root.GetTerrainIncarnation(id); } }
  public void AddObserver(ITerrainEffectObserver observer) {
    root.AddTerrainObserver(id, observer);
  }
  public void RemoveObserver(ITerrainEffectObserver observer) {
    root.RemoveTerrainObserver(id, observer);
  }
  public void Delete() {
    root.EffectTerrainDelete(id);
  }
  public static Terrain Null = new Terrain(null, 0);
  public bool Exists() { return root != null && root.TerrainExists(id); }
  public bool NullableIs(Terrain that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(Terrain that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Pattern pattern {
    get { return incarnation.pattern; }
    set { root.EffectTerrainSetPattern(id, value); }
  }
  public float elevationStepHeight {
    get { return incarnation.elevationStepHeight; }
  }
  public TerrainTileByLocationMutMap tiles {
    get { return new TerrainTileByLocationMutMap(root, incarnation.tiles); }
  }
}
}
