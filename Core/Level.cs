using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Level {
  public readonly Root root;
  public readonly int id;
  public Level(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelIncarnation incarnation { get { return root.GetLevelIncarnation(id); } }
  public void AddObserver(ILevelEffectObserver observer) {
    root.AddLevelObserver(id, observer);
  }
  public void RemoveObserver(ILevelEffectObserver observer) {
    root.RemoveLevelObserver(id, observer);
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
  public bool Is(Level that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string name {
    get { return incarnation.name; }
  }
  public bool considerCornersAdjacent {
    get { return incarnation.considerCornersAdjacent; }
  }
  public Terrain terrain {
    get { return new Terrain(root, incarnation.terrain); }
  }
  public UnitMutBunch units {
    get { return new UnitMutBunch(root, incarnation.units); }
  }
}
}
