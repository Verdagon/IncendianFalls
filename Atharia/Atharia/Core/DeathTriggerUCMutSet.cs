using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DeathTriggerUCMutSet {
  public readonly Root root;
  public readonly int id;
  public DeathTriggerUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DeathTriggerUCMutSetIncarnation incarnation {
    get { return root.GetDeathTriggerUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDeathTriggerUCMutSetEffectObserver observer) {
    broadcaster.AddDeathTriggerUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDeathTriggerUCMutSetEffectObserver observer) {
    broadcaster.RemoveDeathTriggerUCMutSetObserver(id, observer);
  }
  public void Add(DeathTriggerUC element) {
      root.EffectDeathTriggerUCMutSetAdd(id, element.id);
  }
  public void Remove(DeathTriggerUC element) {
      root.EffectDeathTriggerUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDeathTriggerUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectDeathTriggerUCMutSetRemove(id, element);
    }
  }
  public bool Contains(DeathTriggerUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<DeathTriggerUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetDeathTriggerUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DeathTriggerUC>();
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
      if (!root.DeathTriggerUCExists(element.id)) {
        violations.Add("Null constraint violated! DeathTriggerUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DeathTriggerUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
