using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetImpulse {
  public readonly Root root;
  public readonly int id;
  public KamikazeTargetImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeTargetImpulseIncarnation incarnation { get { return root.GetKamikazeTargetImpulseIncarnation(id); } }
  public void AddObserver(IKamikazeTargetImpulseEffectObserver observer) {
    root.AddKamikazeTargetImpulseObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeTargetImpulseEffectObserver observer) {
    root.RemoveKamikazeTargetImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectKamikazeTargetImpulseDelete(id);
  }
  public static KamikazeTargetImpulse Null = new KamikazeTargetImpulse(null, 0);
  public bool Exists() { return root != null && root.KamikazeTargetImpulseExists(id); }
  public bool NullableIs(KamikazeTargetImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.KamikazeAICapabilityUCExists(capability.id)) {
      violations.Add("Null constraint violated! KamikazeTargetImpulse#" + id + ".capability");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.KamikazeAICapabilityUCExists(capability.id)) {
      capability.FindReachableObjects(foundIds);
    }
  }
  public bool Is(KamikazeTargetImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int weight {
    get { return incarnation.weight; }
  }
  public KamikazeAICapabilityUC capability {

    get {
      if (root == null) {
        throw new Exception("Tried to get member capability of null!");
      }
      return new KamikazeAICapabilityUC(root, incarnation.capability);
    }
                       }
  public Location targetLocationCenter {
    get { return incarnation.targetLocationCenter; }
  }
  public LocationImmList targetLocations {
    get { return incarnation.targetLocations; }
  }
}
}
