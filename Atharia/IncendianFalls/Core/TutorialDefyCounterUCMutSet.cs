using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TutorialDefyCounterUCMutSet {
  public readonly Root root;
  public readonly int id;
  public TutorialDefyCounterUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TutorialDefyCounterUCMutSetIncarnation incarnation {
    get { return root.GetTutorialDefyCounterUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITutorialDefyCounterUCMutSetEffectObserver observer) {
    broadcaster.AddTutorialDefyCounterUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITutorialDefyCounterUCMutSetEffectObserver observer) {
    broadcaster.RemoveTutorialDefyCounterUCMutSetObserver(id, observer);
  }
  public void Add(TutorialDefyCounterUC element) {
      root.EffectTutorialDefyCounterUCMutSetAdd(id, element.id);
  }
  public void Remove(TutorialDefyCounterUC element) {
      root.EffectTutorialDefyCounterUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTutorialDefyCounterUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectTutorialDefyCounterUCMutSetRemove(id, element);
    }
  }
  public bool Contains(TutorialDefyCounterUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TutorialDefyCounterUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTutorialDefyCounterUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TutorialDefyCounterUC>();
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
      if (!root.TutorialDefyCounterUCExists(element.id)) {
        violations.Add("Null constraint violated! TutorialDefyCounterUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TutorialDefyCounterUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
