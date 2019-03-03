using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MoveDirectiveUC {
  public readonly Root root;
  public readonly int id;
  public MoveDirectiveUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MoveDirectiveUCIncarnation incarnation { get { return root.GetMoveDirectiveUCIncarnation(id); } }
  public void AddObserver(IMoveDirectiveUCEffectObserver observer) {
    root.AddMoveDirectiveUCObserver(id, observer);
  }
  public void RemoveObserver(IMoveDirectiveUCEffectObserver observer) {
    root.RemoveMoveDirectiveUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectMoveDirectiveUCDelete(id);
  }
  public static MoveDirectiveUC Null = new MoveDirectiveUC(null, 0);
  public bool Exists() { return root != null && root.MoveDirectiveUCExists(id); }
  public bool NullableIs(MoveDirectiveUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.LocationMutListExists(path.id)) {
      violations.Add("Null constraint violated! MoveDirectiveUC#" + id + ".path");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.LocationMutListExists(path.id)) {
      path.FindReachableObjects(foundIds);
    }
  }
  public bool Is(MoveDirectiveUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public LocationMutList path {

    get {
      if (root == null) {
        throw new Exception("Tried to get member path of null!");
      }
      return new LocationMutList(root, incarnation.path);
    }
                       }
}
}
