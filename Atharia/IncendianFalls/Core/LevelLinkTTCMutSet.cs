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
  public void AddObserver(ILevelLinkTTCMutSetEffectObserver observer) {
    root.AddLevelLinkTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ILevelLinkTTCMutSetEffectObserver observer) {
    root.RemoveLevelLinkTTCMutSetObserver(id, observer);
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
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectLevelLinkTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(LevelLinkTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<LevelLinkTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
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