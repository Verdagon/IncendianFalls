using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KillDirectiveUC {
  public readonly Root root;
  public readonly int id;
  public KillDirectiveUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KillDirectiveUCIncarnation incarnation { get { return root.GetKillDirectiveUCIncarnation(id); } }
  public void AddObserver(IKillDirectiveUCEffectObserver observer) {
    root.AddKillDirectiveUCObserver(id, observer);
  }
  public void RemoveObserver(IKillDirectiveUCEffectObserver observer) {
    root.RemoveKillDirectiveUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectKillDirectiveUCDelete(id);
  }
  public static KillDirectiveUC Null = new KillDirectiveUC(null, 0);
  public bool Exists() { return root != null && root.KillDirectiveUCExists(id); }
  public bool NullableIs(KillDirectiveUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.LocationMutListExists(pathToLastSeenLocation.id)) {
      violations.Add("Null constraint violated! KillDirectiveUC#" + id + ".pathToLastSeenLocation");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.UnitExists(targetUnit.id)) {
      targetUnit.FindReachableObjects(foundIds);
    }
    if (root.LocationMutListExists(pathToLastSeenLocation.id)) {
      pathToLastSeenLocation.FindReachableObjects(foundIds);
    }
  }
  public bool Is(KillDirectiveUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Unit targetUnit {

    get {
      if (root == null) {
        throw new Exception("Tried to get member targetUnit of null!");
      }
      return new Unit(root, incarnation.targetUnit);
    }
                       }
  public LocationMutList pathToLastSeenLocation {

    get {
      if (root == null) {
        throw new Exception("Tried to get member pathToLastSeenLocation of null!");
      }
      return new LocationMutList(root, incarnation.pathToLastSeenLocation);
    }
                       }
}
}
