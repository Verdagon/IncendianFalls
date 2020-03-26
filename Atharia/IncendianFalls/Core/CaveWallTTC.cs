using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CaveWallTTC {
  public readonly Root root;
  public readonly int id;
  public CaveWallTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CaveWallTTCIncarnation incarnation { get { return root.GetCaveWallTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICaveWallTTCEffectObserver observer) {
    broadcaster.AddCaveWallTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICaveWallTTCEffectObserver observer) {
    broadcaster.RemoveCaveWallTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectCaveWallTTCDelete(id);
  }
  public static CaveWallTTC Null = new CaveWallTTC(null, 0);
  public bool Exists() { return root != null && root.CaveWallTTCExists(id); }
  public bool NullableIs(CaveWallTTC that) {
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
  public bool Is(CaveWallTTC that) {
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
