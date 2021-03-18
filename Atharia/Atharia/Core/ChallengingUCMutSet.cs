using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ChallengingUCMutSet {
  public readonly Root root;
  public readonly int id;
  public ChallengingUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ChallengingUCMutSetIncarnation incarnation {
    get { return root.GetChallengingUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IChallengingUCMutSetEffectObserver observer) {
    broadcaster.AddChallengingUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IChallengingUCMutSetEffectObserver observer) {
    broadcaster.RemoveChallengingUCMutSetObserver(id, observer);
  }
  public void Add(ChallengingUC element) {
      root.EffectChallengingUCMutSetAdd(id, element.id);
  }
  public void Remove(ChallengingUC element) {
      root.EffectChallengingUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectChallengingUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectChallengingUCMutSetRemove(id, element);
    }
  }
  public bool Contains(ChallengingUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<ChallengingUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetChallengingUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<ChallengingUC>();
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
      if (!root.ChallengingUCExists(element.id)) {
        violations.Add("Null constraint violated! ChallengingUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ChallengingUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
