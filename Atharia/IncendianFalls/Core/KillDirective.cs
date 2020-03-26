using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KillDirective {
  public readonly Root root;
  public readonly int id;
  public KillDirective(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public KillDirectiveIncarnation incarnation { get { return root.GetKillDirectiveIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IKillDirectiveEffectObserver observer) {
    broadcaster.AddKillDirectiveObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IKillDirectiveEffectObserver observer) {
    broadcaster.RemoveKillDirectiveObserver(id, observer);
  }
  public void Delete() {
    root.EffectKillDirectiveDelete(id);
  }
  public static KillDirective Null = new KillDirective(null, 0);
  public bool Exists() { return root != null && root.KillDirectiveExists(id); }
  public bool NullableIs(KillDirective that) {
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
      violations.Add("Null constraint violated! KillDirective#" + id + ".pathToLastSeenLocation");
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
  public bool Is(KillDirective that) {
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
