using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireBombTTC {
  public readonly Root root;
  public readonly int id;
  public FireBombTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireBombTTCIncarnation incarnation { get { return root.GetFireBombTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IFireBombTTCEffectObserver observer) {
    broadcaster.AddFireBombTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireBombTTCEffectObserver observer) {
    broadcaster.RemoveFireBombTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectFireBombTTCDelete(id);
  }
  public static FireBombTTC Null = new FireBombTTC(null, 0);
  public bool Exists() { return root != null && root.FireBombTTCExists(id); }
  public bool NullableIs(FireBombTTC that) {
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
  public bool Is(FireBombTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int turnsUntilExplosion {
    get { return incarnation.turnsUntilExplosion; }
    set { root.EffectFireBombTTCSetTurnsUntilExplosion(id, value); }
  }
}
}
