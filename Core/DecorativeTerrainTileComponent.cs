using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DecorativeTerrainTileComponent {
  public readonly Root root;
  public readonly int id;
  public DecorativeTerrainTileComponent(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DecorativeTerrainTileComponentIncarnation incarnation { get { return root.GetDecorativeTerrainTileComponentIncarnation(id); } }
  public void AddObserver(IDecorativeTerrainTileComponentEffectObserver observer) {
    root.AddDecorativeTerrainTileComponentObserver(id, observer);
  }
  public void RemoveObserver(IDecorativeTerrainTileComponentEffectObserver observer) {
    root.RemoveDecorativeTerrainTileComponentObserver(id, observer);
  }
  public void Delete() {
    root.EffectDecorativeTerrainTileComponentDelete(id);
  }
  public static DecorativeTerrainTileComponent Null = new DecorativeTerrainTileComponent(null, 0);
  public bool Exists() { return root != null && root.DecorativeTerrainTileComponentExists(id); }
  public bool NullableIs(DecorativeTerrainTileComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(DecorativeTerrainTileComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string symbolId {
    get { return incarnation.symbolId; }
  }
}
}
