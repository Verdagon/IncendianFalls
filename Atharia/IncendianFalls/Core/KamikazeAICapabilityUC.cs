using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public KamikazeAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeAICapabilityUCIncarnation incarnation { get { return root.GetKamikazeAICapabilityUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IKamikazeAICapabilityUCEffectObserver observer) {
    broadcaster.AddKamikazeAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IKamikazeAICapabilityUCEffectObserver observer) {
    broadcaster.RemoveKamikazeAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectKamikazeAICapabilityUCDelete(id);
  }
  public static KamikazeAICapabilityUC Null = new KamikazeAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.KamikazeAICapabilityUCExists(id); }
  public bool NullableIs(KamikazeAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.KamikazeTargetTTCStrongByLocationMutMapExists(targetByLocation.id)) {
      violations.Add("Null constraint violated! KamikazeAICapabilityUC#" + id + ".targetByLocation");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.KamikazeTargetTTCStrongByLocationMutMapExists(targetByLocation.id)) {
      targetByLocation.FindReachableObjects(foundIds);
    }
  }
  public bool Is(KamikazeAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public KamikazeTargetTTCStrongByLocationMutMap targetByLocation {

    get {
      if (root == null) {
        throw new Exception("Tried to get member targetByLocation of null!");
      }
      return new KamikazeTargetTTCStrongByLocationMutMap(root, incarnation.targetByLocation);
    }
                         set { root.EffectKamikazeAICapabilityUCSetTargetByLocation(id, value); }
  }
  public Location targetLocationCenter {
    get { return incarnation.targetLocationCenter; }
    set { root.EffectKamikazeAICapabilityUCSetTargetLocationCenter(id, value); }
  }
}
}
