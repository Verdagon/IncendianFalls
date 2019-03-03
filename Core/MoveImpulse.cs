using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MoveImpulse {
  public readonly Root root;
  public readonly int id;
  public MoveImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MoveImpulseIncarnation incarnation { get { return root.GetMoveImpulseIncarnation(id); } }
  public void AddObserver(IMoveImpulseEffectObserver observer) {
    root.AddMoveImpulseObserver(id, observer);
  }
  public void RemoveObserver(IMoveImpulseEffectObserver observer) {
    root.RemoveMoveImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectMoveImpulseDelete(id);
  }
  public static MoveImpulse Null = new MoveImpulse(null, 0);
  public bool Exists() { return root != null && root.MoveImpulseExists(id); }
  public bool NullableIs(MoveImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(MoveImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int weight {
    get { return incarnation.weight; }
  }
  public Location stepLocation {
    get { return incarnation.stepLocation; }
  }
}
}
