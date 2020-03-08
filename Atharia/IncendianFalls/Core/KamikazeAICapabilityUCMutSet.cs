using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public KamikazeAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KamikazeAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetKamikazeAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(IKamikazeAICapabilityUCMutSetEffectObserver observer) {
    root.AddKamikazeAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IKamikazeAICapabilityUCMutSetEffectObserver observer) {
    root.RemoveKamikazeAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(KamikazeAICapabilityUC element) {
    root.EffectKamikazeAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(KamikazeAICapabilityUC element) {
    root.EffectKamikazeAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectKamikazeAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectKamikazeAICapabilityUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(KamikazeAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<KamikazeAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetKamikazeAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<KamikazeAICapabilityUC>();
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
      if (!root.KamikazeAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! KamikazeAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.KamikazeAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
