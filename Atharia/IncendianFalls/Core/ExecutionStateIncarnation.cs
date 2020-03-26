using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExecutionStateIncarnation : IExecutionStateEffectVisitor {
  public  int actingUnit;
  public  bool actingUnitDidAction;
  public  int remainingPreActingUnitComponents;
  public  int remainingPostActingUnitComponents;
  public ExecutionStateIncarnation(
      int actingUnit,
      bool actingUnitDidAction,
      int remainingPreActingUnitComponents,
      int remainingPostActingUnitComponents) {
    this.actingUnit = actingUnit;
    this.actingUnitDidAction = actingUnitDidAction;
    this.remainingPreActingUnitComponents = remainingPreActingUnitComponents;
    this.remainingPostActingUnitComponents = remainingPostActingUnitComponents;
  }
  public ExecutionStateIncarnation Copy() {
    return new ExecutionStateIncarnation(
actingUnit,
actingUnitDidAction,
remainingPreActingUnitComponents,
remainingPostActingUnitComponents    );
  }

  public void visitExecutionStateCreateEffect(ExecutionStateCreateEffect e) {}
  public void visitExecutionStateDeleteEffect(ExecutionStateDeleteEffect e) {}
public void visitExecutionStateSetActingUnitEffect(ExecutionStateSetActingUnitEffect e) { this.actingUnit = e.newValue; }
public void visitExecutionStateSetActingUnitDidActionEffect(ExecutionStateSetActingUnitDidActionEffect e) { this.actingUnitDidAction = e.newValue; }
public void visitExecutionStateSetRemainingPreActingUnitComponentsEffect(ExecutionStateSetRemainingPreActingUnitComponentsEffect e) { this.remainingPreActingUnitComponents = e.newValue; }
public void visitExecutionStateSetRemainingPostActingUnitComponentsEffect(ExecutionStateSetRemainingPostActingUnitComponentsEffect e) { this.remainingPostActingUnitComponents = e.newValue; }
  public void ApplyEffect(IExecutionStateEffect effect) { effect.visitIExecutionStateEffect(this); }
}

}
