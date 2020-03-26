using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ExecutionState {
  public readonly Root root;
  public readonly int id;
  public ExecutionState(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ExecutionStateIncarnation incarnation { get { return root.GetExecutionStateIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IExecutionStateEffectObserver observer) {
    broadcaster.AddExecutionStateObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IExecutionStateEffectObserver observer) {
    broadcaster.RemoveExecutionStateObserver(id, observer);
  }
  public void Delete() {
    root.EffectExecutionStateDelete(id);
  }
  public static ExecutionState Null = new ExecutionState(null, 0);
  public bool Exists() { return root != null && root.ExecutionStateExists(id); }
  public bool NullableIs(ExecutionState that) {
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
    if (root.UnitExists(actingUnit.id)) {
      actingUnit.FindReachableObjects(foundIds);
    }
    if (root.IPreActingUCWeakMutBunchExists(remainingPreActingUnitComponents.id)) {
      remainingPreActingUnitComponents.FindReachableObjects(foundIds);
    }
    if (root.IPostActingUCWeakMutBunchExists(remainingPostActingUnitComponents.id)) {
      remainingPostActingUnitComponents.FindReachableObjects(foundIds);
    }
  }
  public bool Is(ExecutionState that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Unit actingUnit {

    get {
      if (root == null) {
        throw new Exception("Tried to get member actingUnit of null!");
      }
      return new Unit(root, incarnation.actingUnit);
    }
                         set { root.EffectExecutionStateSetActingUnit(id, value); }
  }
  public bool actingUnitDidAction {
    get { return incarnation.actingUnitDidAction; }
    set { root.EffectExecutionStateSetActingUnitDidAction(id, value); }
  }
  public IPreActingUCWeakMutBunch remainingPreActingUnitComponents {

    get {
      if (root == null) {
        throw new Exception("Tried to get member remainingPreActingUnitComponents of null!");
      }
      return new IPreActingUCWeakMutBunch(root, incarnation.remainingPreActingUnitComponents);
    }
                         set { root.EffectExecutionStateSetRemainingPreActingUnitComponents(id, value); }
  }
  public IPostActingUCWeakMutBunch remainingPostActingUnitComponents {

    get {
      if (root == null) {
        throw new Exception("Tried to get member remainingPostActingUnitComponents of null!");
      }
      return new IPostActingUCWeakMutBunch(root, incarnation.remainingPostActingUnitComponents);
    }
                         set { root.EffectExecutionStateSetRemainingPostActingUnitComponents(id, value); }
  }
}
}
