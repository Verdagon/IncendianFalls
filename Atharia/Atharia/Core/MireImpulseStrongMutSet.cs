using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MireImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public MireImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MireImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetMireImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IMireImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddMireImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMireImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveMireImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(MireImpulse element) {
      root.EffectMireImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(MireImpulse element) {
      root.EffectMireImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMireImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectMireImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(MireImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<MireImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetMireImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<MireImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.MireImpulseExists(element.id)) {
        violations.Add("Null constraint violated! MireImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MireImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
