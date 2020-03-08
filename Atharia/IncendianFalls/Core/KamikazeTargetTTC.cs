using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetTTC {
  public readonly Root root;
  public readonly int id;
  public KamikazeTargetTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeTargetTTCIncarnation incarnation { get { return root.GetKamikazeTargetTTCIncarnation(id); } }
  public void AddObserver(IKamikazeTargetTTCEffectObserver observer) {
    root.AddKamikazeTargetTTCObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeTargetTTCEffectObserver observer) {
    root.RemoveKamikazeTargetTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectKamikazeTargetTTCDelete(id);
  }
  public static KamikazeTargetTTC Null = new KamikazeTargetTTC(null, 0);
  public bool Exists() { return root != null && root.KamikazeTargetTTCExists(id); }
  public bool NullableIs(KamikazeTargetTTC that) {
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
      violations.Add("Null constraint violated! KamikazeTargetTTC#" + id + ".capability");
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
  public bool Is(KamikazeTargetTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public KamikazeAICapabilityUC capability {

    get {
      if (root == null) {
        throw new Exception("Tried to get member capability of null!");
      }
      return new KamikazeAICapabilityUC(root, incarnation.capability);
    }
                       }
}
}
