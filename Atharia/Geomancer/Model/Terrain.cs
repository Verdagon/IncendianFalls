using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

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
  public void CheckForNullViolations(List<string> violations) {

    if (!root.TerrainTileByLocationMutMapExists(tiles.id)) {
      violations.Add("Null constraint violated! Terrain#" + id + ".tiles");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.TerrainTileByLocationMutMapExists(tiles.id)) {
      tiles.FindReachableObjects(foundIds);
    }
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
  }
  public float elevationStepHeight {
    get { return incarnation.elevationStepHeight; }
  }
  public TerrainTileByLocationMutMap tiles {

    get {
      if (root == null) {
        throw new Exception("Tried to get member tiles of null!");
      }
      return new TerrainTileByLocationMutMap(root, incarnation.tiles);
    }
                       }
}
}
