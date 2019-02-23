using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KillDirectiveUCMutSet {
  public readonly Root root;
  public readonly int id;
  public KillDirectiveUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KillDirectiveUCMutSetIncarnation incarnation {
    get { return root.GetKillDirectiveUCMutSetIncarnation(id); }
  }
  public void AddObserver(IKillDirectiveUCMutSetEffectObserver observer) {
    root.AddKillDirectiveUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IKillDirectiveUCMutSetEffectObserver observer) {
    root.RemoveKillDirectiveUCMutSetObserver(id, observer);
  }
  public void Add(KillDirectiveUC element) {
    root.EffectKillDirectiveUCMutSetAdd(id, element.id);
  }
  public void Remove(KillDirectiveUC element) {
    root.EffectKillDirectiveUCMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectKillDirectiveUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<KillDirectiveUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetKillDirectiveUC(element);
    }
  }
}
         
}
