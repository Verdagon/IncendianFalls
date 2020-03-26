using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public KamikazeTargetImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeTargetImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetKamikazeTargetImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IKamikazeTargetImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddKamikazeTargetImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IKamikazeTargetImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveKamikazeTargetImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(KamikazeTargetImpulse element) {
      root.EffectKamikazeTargetImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(KamikazeTargetImpulse element) {
      root.EffectKamikazeTargetImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectKamikazeTargetImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectKamikazeTargetImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(KamikazeTargetImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<KamikazeTargetImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetKamikazeTargetImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<KamikazeTargetImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.KamikazeTargetImpulseExists(element.id)) {
        violations.Add("Null constraint violated! KamikazeTargetImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.KamikazeTargetImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
