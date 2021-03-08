using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public MoveImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MoveImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetMoveImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IMoveImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddMoveImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMoveImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveMoveImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(MoveImpulse element) {
      root.EffectMoveImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(MoveImpulse element) {
      root.EffectMoveImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMoveImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectMoveImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(MoveImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<MoveImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetMoveImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<MoveImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.MoveImpulseExists(element.id)) {
        violations.Add("Null constraint violated! MoveImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MoveImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
