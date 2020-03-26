using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LightningChargingUC {
  public readonly Root root;
  public readonly int id;
  public LightningChargingUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LightningChargingUCIncarnation incarnation { get { return root.GetLightningChargingUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILightningChargingUCEffectObserver observer) {
    broadcaster.AddLightningChargingUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILightningChargingUCEffectObserver observer) {
    broadcaster.RemoveLightningChargingUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectLightningChargingUCDelete(id);
  }
  public static LightningChargingUC Null = new LightningChargingUC(null, 0);
  public bool Exists() { return root != null && root.LightningChargingUCExists(id); }
  public bool NullableIs(LightningChargingUC that) {
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
  public bool Is(LightningChargingUC that) {
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
