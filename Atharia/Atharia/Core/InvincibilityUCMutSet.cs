using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public InvincibilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InvincibilityUCMutSetIncarnation incarnation {
    get { return root.GetInvincibilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IInvincibilityUCMutSetEffectObserver observer) {
    broadcaster.AddInvincibilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IInvincibilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveInvincibilityUCMutSetObserver(id, observer);
  }
  public void Add(InvincibilityUC element) {
      root.EffectInvincibilityUCMutSetAdd(id, element.id);
  }
  public void Remove(InvincibilityUC element) {
      root.EffectInvincibilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectInvincibilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectInvincibilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(InvincibilityUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<InvincibilityUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetInvincibilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<InvincibilityUC>();
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
      if (!root.InvincibilityUCExists(element.id)) {
        violations.Add("Null constraint violated! InvincibilityUCMutSet#" + id + "." + element.id);
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
