using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public InvincibilityUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InvincibilityUCWeakMutSetIncarnation incarnation {
    get { return root.GetInvincibilityUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IInvincibilityUCWeakMutSetEffectObserver observer) {
    broadcaster.AddInvincibilityUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IInvincibilityUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveInvincibilityUCWeakMutSetObserver(id, observer);
  }
  public void Add(InvincibilityUC element) {
      root.EffectInvincibilityUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(InvincibilityUC element) {
      root.EffectInvincibilityUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectInvincibilityUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectInvincibilityUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(InvincibilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<InvincibilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetInvincibilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<InvincibilityUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.InvincibilityUCExists(element.id)) {
        violations.Add("Null constraint violated! InvincibilityUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.InvincibilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
