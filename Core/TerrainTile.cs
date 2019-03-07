using System;
using System.Collections;

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
  public void CheckForNullViolations(List<string> violations) {

    if (!root.ITerrainTileComponentMutBunchExists(components.id)) {
      violations.Add("Null constraint violated! TerrainTile#" + id + ".components");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ITerrainTileComponentMutBunchExists(components.id)) {
      components.FindReachableObjects(foundIds);
    }
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
    set { root.EffectTerrainTileSetWalkable(id, value); }
  }
  public string classId {
    get { return incarnation.classId; }
    set { root.EffectTerrainTileSetClassId(id, value); }
  }
  public ITerrainTileComponentMutBunch components {

    get {
      if (root == null) {
        throw new Exception("Tried to get member components of null!");
      }
      return new ITerrainTileComponentMutBunch(root, incarnation.components);
    }
                       }
}
}
