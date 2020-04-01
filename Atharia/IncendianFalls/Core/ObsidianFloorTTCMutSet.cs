using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianFloorTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public ObsidianFloorTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ObsidianFloorTTCMutSetIncarnation incarnation {
    get { return root.GetObsidianFloorTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IObsidianFloorTTCMutSetEffectObserver observer) {
    broadcaster.AddObsidianFloorTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IObsidianFloorTTCMutSetEffectObserver observer) {
    broadcaster.RemoveObsidianFloorTTCMutSetObserver(id, observer);
  }
  public void Add(ObsidianFloorTTC element) {
      root.EffectObsidianFloorTTCMutSetAdd(id, element.id);
  }
  public void Remove(ObsidianFloorTTC element) {
      root.EffectObsidianFloorTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectObsidianFloorTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectObsidianFloorTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(ObsidianFloorTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<ObsidianFloorTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetObsidianFloorTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<ObsidianFloorTTC>();
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
      if (!root.ObsidianFloorTTCExists(element.id)) {
        violations.Add("Null constraint violated! ObsidianFloorTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ObsidianFloorTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
