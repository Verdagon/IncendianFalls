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
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMoveDirectiveUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<MoveDirectiveUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetMoveDirectiveUC(element);
    }
  }
}
         
}
