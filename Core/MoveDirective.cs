using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class MoveDirective {
  public readonly Root root;
  public readonly int id;
  public MoveDirective(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MoveDirectiveIncarnation incarnation { get { return root.GetMoveDirectiveIncarnation(id); } }
  public void AddObserver(IMoveDirectiveEffectObserver observer) {
    root.AddMoveDirectiveObserver(id, observer);
  }
  public void RemoveObserver(IMoveDirectiveEffectObserver observer) {
    root.RemoveMoveDirectiveObserver(id, observer);
  }
  public void Delete() {
    root.EffectMoveDirectiveDelete(id);
  }
  public static MoveDirective Null = new MoveDirective(null, 0);
  public bool Exists() { return root != null && root.MoveDirectiveExists(id); }
  public bool NullableIs(MoveDirective that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(MoveDirective that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public LocationMutList path {
    get { return new LocationMutList(root, incarnation.path); }
  }
}
}
