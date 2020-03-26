using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelMutSet {
  public readonly Root root;
  public readonly int id;
  public LevelMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelMutSetIncarnation incarnation {
    get { return root.GetLevelMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ILevelMutSetEffectObserver observer) {
    broadcaster.AddLevelMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILevelMutSetEffectObserver observer) {
    broadcaster.RemoveLevelMutSetObserver(id, observer);
  }
  public void Add(Level element) {
      root.EffectLevelMutSetAdd(id, element.id);
  }
  public void Remove(Level element) {
      root.EffectLevelMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectLevelMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectLevelMutSetRemove(id, element);
    }
  }
  public bool Contains(Level element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Level> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetLevel(element);
    }
  }
  public void Destruct() {
    var elements = new List<Level>();
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
      if (!root.LevelExists(element.id)) {
        violations.Add("Null constraint violated! LevelMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.LevelExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
