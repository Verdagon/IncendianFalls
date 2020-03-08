using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeJumpImpulse {
  public readonly Root root;
  public readonly int id;
  public KamikazeJumpImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeJumpImpulseIncarnation incarnation { get { return root.GetKamikazeJumpImpulseIncarnation(id); } }
  public void AddObserver(IKamikazeJumpImpulseEffectObserver observer) {
    root.AddKamikazeJumpImpulseObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeJumpImpulseEffectObserver observer) {
    root.RemoveKamikazeJumpImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectKamikazeJumpImpulseDelete(id);
  }
  public static KamikazeJumpImpulse Null = new KamikazeJumpImpulse(null, 0);
  public bool Exists() { return root != null && root.KamikazeJumpImpulseExists(id); }
  public bool NullableIs(KamikazeJumpImpulse that) {
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
      violations.Add("Null constraint violated! KamikazeJumpImpulse#" + id + ".capability");
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
  public bool Is(KamikazeJumpImpulse that) {
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
  public Location jumpTarget {
    get { return incarnation.jumpTarget; }
  }
}
}
