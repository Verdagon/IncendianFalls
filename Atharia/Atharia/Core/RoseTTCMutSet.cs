using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RoseTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public RoseTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RoseTTCMutSetIncarnation incarnation {
    get { return root.GetRoseTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IRoseTTCMutSetEffectObserver observer) {
    broadcaster.AddRoseTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRoseTTCMutSetEffectObserver observer) {
    broadcaster.RemoveRoseTTCMutSetObserver(id, observer);
  }
  public void Add(RoseTTC element) {
      root.EffectRoseTTCMutSetAdd(id, element.id);
  }
  public void Remove(RoseTTC element) {
      root.EffectRoseTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectRoseTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectRoseTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(RoseTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<RoseTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetRoseTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<RoseTTC>();
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
      if (!root.RoseTTCExists(element.id)) {
        violations.Add("Null constraint violated! RoseTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.RoseTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
