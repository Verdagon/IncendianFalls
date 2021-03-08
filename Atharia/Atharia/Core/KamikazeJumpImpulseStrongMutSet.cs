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
  public void AddObserver(EffectBroadcaster broadcaster, IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddKamikazeJumpImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveKamikazeJumpImpulseStrongMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectKamikazeJumpImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(KamikazeJumpImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<KamikazeJumpImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
