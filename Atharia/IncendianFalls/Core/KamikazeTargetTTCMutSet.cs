using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public KamikazeTargetTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeTargetTTCMutSetIncarnation incarnation {
    get { return root.GetKamikazeTargetTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IKamikazeTargetTTCMutSetEffectObserver observer) {
    root.AddKamikazeTargetTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeTargetTTCMutSetEffectObserver observer) {
    root.RemoveKamikazeTargetTTCMutSetObserver(id, observer);
  }
  public void Add(KamikazeTargetTTC element) {
    root.EffectKamikazeTargetTTCMutSetAdd(id, element.id);
  }
  public void Remove(KamikazeTargetTTC element) {
    root.EffectKamikazeTargetTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectKamikazeTargetTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectKamikazeTargetTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(KamikazeTargetTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<KamikazeTargetTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetKamikazeTargetTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<KamikazeTargetTTC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.KamikazeTargetTTCExists(element.id)) {
        violations.Add("Null constraint violated! KamikazeTargetTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.KamikazeTargetTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
