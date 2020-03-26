using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LightningChargedUC {
  public readonly Root root;
  public readonly int id;
  public LightningChargedUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LightningChargedUCIncarnation incarnation { get { return root.GetLightningChargedUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILightningChargedUCEffectObserver observer) {
    broadcaster.AddLightningChargedUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILightningChargedUCEffectObserver observer) {
    broadcaster.RemoveLightningChargedUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectLightningChargedUCDelete(id);
  }
  public static LightningChargedUC Null = new LightningChargedUC(null, 0);
  public bool Exists() { return root != null && root.LightningChargedUCExists(id); }
  public bool NullableIs(LightningChargedUC that) {
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
  public bool Is(LightningChargedUC that) {
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
