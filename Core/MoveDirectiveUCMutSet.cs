using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveDirectiveUCMutSet {
  public readonly Root root;
  public readonly int id;
  public MoveDirectiveUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MoveDirectiveUCMutSetIncarnation incarnation {
    get { return root.GetMoveDirectiveUCMutSetIncarnation(id); }
  }
  public void AddObserver(IMoveDirectiveUCMutSetEffectObserver observer) {
    root.AddMoveDirectiveUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IMoveDirectiveUCMutSetEffectObserver observer) {
    root.RemoveMoveDirectiveUCMutSetObserver(id, observer);
  }
  public void Add(MoveDirectiveUC element) {
    root.EffectMoveDirectiveUCMutSetAdd(id, element.id);
  }
  public void Remove(MoveDirectiveUC element) {
    root.EffectMoveDirectiveUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMoveDirectiveUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMoveDirectiveUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<MoveDirectiveUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetMoveDirectiveUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MoveDirectiveUC>();
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
      if (!root.MoveDirectiveUCExists(element.id)) {
        violations.Add("Null constraint violated! MoveDirectiveUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MoveDirectiveUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
