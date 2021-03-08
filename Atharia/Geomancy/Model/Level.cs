using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class Level {
  public readonly Root root;
  public readonly int id;
  public Level(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelIncarnation incarnation { get { return root.GetLevelIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILevelEffectObserver observer) {
    broadcaster.AddLevelObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILevelEffectObserver observer) {
    broadcaster.RemoveLevelObserver(id, observer);
  }
  public void Delete() {
    root.EffectLevelDelete(id);
  }
  public static Level Null = new Level(null, 0);
  public bool Exists() { return root != null && root.LevelExists(id); }
  public bool NullableIs(Level that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.TerrainExists(terrain.id)) {
      violations.Add("Null constraint violated! Level#" + id + ".terrain");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.TerrainExists(terrain.id)) {
      terrain.FindReachableObjects(foundIds);
    }
  }
  public bool Is(Level that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Terrain terrain {

    get {
      if (root == null) {
        throw new Exception("Tried to get member terrain of null!");
      }
      return new Terrain(root, incarnation.terrain);
    }
                       }
}
}
