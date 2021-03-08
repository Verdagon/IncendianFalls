using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public LevelLinkTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelLinkTTCMutSetIncarnation incarnation {
    get { return root.GetLevelLinkTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILevelLinkTTCMutSetEffectObserver observer) {
    broadcaster.AddLevelLinkTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILevelLinkTTCMutSetEffectObserver observer) {
    broadcaster.RemoveLevelLinkTTCMutSetObserver(id, observer);
  }
  public void Add(LevelLinkTTC element) {
      root.EffectLevelLinkTTCMutSetAdd(id, element.id);
  }
  public void Remove(LevelLinkTTC element) {
      root.EffectLevelLinkTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLevelLinkTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectLevelLinkTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(LevelLinkTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<LevelLinkTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetLevelLinkTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<LevelLinkTTC>();
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
      if (!root.LevelLinkTTCExists(element.id)) {
        violations.Add("Null constraint violated! LevelLinkTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LevelLinkTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
