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
  public void AddObserver(EffectBroadcaster broadcaster, ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
    broadcaster.AddSimplePresenceTriggerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveSimplePresenceTriggerTTCMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectSimplePresenceTriggerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(SimplePresenceTriggerTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<SimplePresenceTriggerTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
