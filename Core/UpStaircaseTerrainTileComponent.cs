using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpStaircaseTerrainTileComponent {
  public readonly Root root;
  public readonly int id;
  public UpStaircaseTerrainTileComponent(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStaircaseTerrainTileComponentIncarnation incarnation { get { return root.GetUpStaircaseTerrainTileComponentIncarnation(id); } }
  public void AddObserver(IUpStaircaseTerrainTileComponentEffectObserver observer) {
    root.AddUpStaircaseTerrainTileComponentObserver(id, observer);
  }
  public void RemoveObserver(IUpStaircaseTerrainTileComponentEffectObserver observer) {
    root.RemoveUpStaircaseTerrainTileComponentObserver(id, observer);
  }
  public void Delete() {
    root.EffectUpStaircaseTerrainTileComponentDelete(id);
  }
  public static UpStaircaseTerrainTileComponent Null = new UpStaircaseTerrainTileComponent(null, 0);
  public bool Exists() { return root != null && root.UpStaircaseTerrainTileComponentExists(id); }
  public bool NullableIs(UpStaircaseTerrainTileComponent that) {
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
  public bool Is(UpStaircaseTerrainTileComponent that) {
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
