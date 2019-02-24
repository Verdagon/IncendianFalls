using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DownStaircaseTerrainTileComponent {
  public readonly Root root;
  public readonly int id;
  public DownStaircaseTerrainTileComponent(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStaircaseTerrainTileComponentIncarnation incarnation { get { return root.GetDownStaircaseTerrainTileComponentIncarnation(id); } }
  public void AddObserver(IDownStaircaseTerrainTileComponentEffectObserver observer) {
    root.AddDownStaircaseTerrainTileComponentObserver(id, observer);
  }
  public void RemoveObserver(IDownStaircaseTerrainTileComponentEffectObserver observer) {
    root.RemoveDownStaircaseTerrainTileComponentObserver(id, observer);
  }
  public void Delete() {
    root.EffectDownStaircaseTerrainTileComponentDelete(id);
  }
  public static DownStaircaseTerrainTileComponent Null = new DownStaircaseTerrainTileComponent(null, 0);
  public bool Exists() { return root != null && root.DownStaircaseTerrainTileComponentExists(id); }
  public bool NullableIs(DownStaircaseTerrainTileComponent that) {
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
  public bool Is(DownStaircaseTerrainTileComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
