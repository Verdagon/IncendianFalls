using System;
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
  public void AddObserver(IExecutionStateEffectObserver observer) {
    root.AddExecutionStateObserver(id, observer);
  }
  public void RemoveObserver(IExecutionStateEffectObserver observer) {
    root.RemoveExecutionStateObserver(id, observer);
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
    get { return new Unit(root, incarnation.actingUnit); }
    set { root.EffectExecutionStateSetActingUnit(id, value); }
  }
  public bool actingUnitDidAction {
    get { return incarnation.actingUnitDidAction; }
    set { root.EffectExecutionStateSetActingUnitDidAction(id, value); }
  }
  public IDetailMutList remainingPreActingDetails {
    get { return new IDetailMutList(root, incarnation.remainingPreActingDetails); }
    set { root.EffectExecutionStateSetRemainingPreActingDetails(id, value); }
  }
  public IDetailMutList remainingPostActingDetails {
    get { return new IDetailMutList(root, incarnation.remainingPostActingDetails); }
    set { root.EffectExecutionStateSetRemainingPostActingDetails(id, value); }
  }
}
}
