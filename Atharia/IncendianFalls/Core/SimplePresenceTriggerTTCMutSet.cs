using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SimplePresenceTriggerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public SimplePresenceTriggerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SimplePresenceTriggerTTCMutSetIncarnation incarnation {
    get { return root.GetSimplePresenceTriggerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
    root.AddSimplePresenceTriggerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
    root.RemoveSimplePresenceTriggerTTCMutSetObserver(id, observer);
  }
  public void Add(SimplePresenceTriggerTTC element) {
    root.EffectSimplePresenceTriggerTTCMutSetAdd(id, element.id);
  }
  public void Remove(SimplePresenceTriggerTTC element) {
    root.EffectSimplePresenceTriggerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSimplePresenceTriggerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSimplePresenceTriggerTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(SimplePresenceTriggerTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<SimplePresenceTriggerTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetSimplePresenceTriggerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<SimplePresenceTriggerTTC>();
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
      if (!root.SimplePresenceTriggerTTCExists(element.id)) {
        violations.Add("Null constraint violated! SimplePresenceTriggerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SimplePresenceTriggerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
