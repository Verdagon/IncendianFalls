using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeAnchorTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public TimeAnchorTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeAnchorTTCMutSetIncarnation incarnation {
    get { return root.GetTimeAnchorTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITimeAnchorTTCMutSetEffectObserver observer) {
    broadcaster.AddTimeAnchorTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITimeAnchorTTCMutSetEffectObserver observer) {
    broadcaster.RemoveTimeAnchorTTCMutSetObserver(id, observer);
  }
  public void Add(TimeAnchorTTC element) {
      root.EffectTimeAnchorTTCMutSetAdd(id, element.id);
  }
  public void Remove(TimeAnchorTTC element) {
      root.EffectTimeAnchorTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTimeAnchorTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectTimeAnchorTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(TimeAnchorTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<TimeAnchorTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetTimeAnchorTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TimeAnchorTTC>();
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
      if (!root.TimeAnchorTTCExists(element.id)) {
        violations.Add("Null constraint violated! TimeAnchorTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TimeAnchorTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
