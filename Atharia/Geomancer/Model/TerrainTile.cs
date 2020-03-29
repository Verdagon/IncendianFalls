using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class TerrainTile {
  public readonly Root root;
  public readonly int id;
  public TerrainTile(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TerrainTileIncarnation incarnation { get { return root.GetTerrainTileIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ITerrainTileEffectObserver observer) {
    broadcaster.AddTerrainTileObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITerrainTileEffectObserver observer) {
    broadcaster.RemoveTerrainTileObserver(id, observer);
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

    if (!root.StrMutListExists(members.id)) {
      violations.Add("Null constraint violated! TerrainTile#" + id + ".members");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.StrMutListExists(members.id)) {
      members.FindReachableObjects(foundIds);
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
  public StrMutList members {

    get {
      if (root == null) {
        throw new Exception("Tried to get member members of null!");
      }
      return new StrMutList(root, incarnation.members);
    }
                       }
}
}
