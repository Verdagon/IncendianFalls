using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaArcanaLevelLinkerTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public RavaArcanaLevelLinkerTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RavaArcanaLevelLinkerTTCMutSetIncarnation incarnation {
    get { return root.GetRavaArcanaLevelLinkerTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IRavaArcanaLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.AddRavaArcanaLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRavaArcanaLevelLinkerTTCMutSetEffectObserver observer) {
    broadcaster.RemoveRavaArcanaLevelLinkerTTCMutSetObserver(id, observer);
  }
  public void Add(RavaArcanaLevelLinkerTTC element) {
      root.EffectRavaArcanaLevelLinkerTTCMutSetAdd(id, element.id);
  }
  public void Remove(RavaArcanaLevelLinkerTTC element) {
      root.EffectRavaArcanaLevelLinkerTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectRavaArcanaLevelLinkerTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectRavaArcanaLevelLinkerTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(RavaArcanaLevelLinkerTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<RavaArcanaLevelLinkerTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetRavaArcanaLevelLinkerTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<RavaArcanaLevelLinkerTTC>();
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
      if (!root.RavaArcanaLevelLinkerTTCExists(element.id)) {
        violations.Add("Null constraint violated! RavaArcanaLevelLinkerTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.RavaArcanaLevelLinkerTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
