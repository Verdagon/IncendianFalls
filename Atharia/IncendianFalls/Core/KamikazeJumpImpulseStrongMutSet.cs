using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeJumpImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public KamikazeJumpImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeJumpImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetKamikazeJumpImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
    root.AddKamikazeJumpImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
    root.RemoveKamikazeJumpImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(KamikazeJumpImpulse element) {
    root.EffectKamikazeJumpImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(KamikazeJumpImpulse element) {
    root.EffectKamikazeJumpImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectKamikazeJumpImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectKamikazeJumpImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(KamikazeJumpImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<KamikazeJumpImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetKamikazeJumpImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<KamikazeJumpImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.KamikazeJumpImpulseExists(element.id)) {
        violations.Add("Null constraint violated! KamikazeJumpImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.KamikazeJumpImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
