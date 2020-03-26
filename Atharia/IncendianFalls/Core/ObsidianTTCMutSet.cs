using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public ObsidianTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ObsidianTTCMutSetIncarnation incarnation {
    get { return root.GetObsidianTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IObsidianTTCMutSetEffectObserver observer) {
    broadcaster.AddObsidianTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IObsidianTTCMutSetEffectObserver observer) {
    broadcaster.RemoveObsidianTTCMutSetObserver(id, observer);
  }
  public void Add(ObsidianTTC element) {
      root.EffectObsidianTTCMutSetAdd(id, element.id);
  }
  public void Remove(ObsidianTTC element) {
      root.EffectObsidianTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectObsidianTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectObsidianTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(ObsidianTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ObsidianTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetObsidianTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<ObsidianTTC>();
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
      if (!root.ObsidianTTCExists(element.id)) {
        violations.Add("Null constraint violated! ObsidianTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ObsidianTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
