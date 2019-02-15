using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IExecutionStateEffectVisitor {
  void visitExecutionStateCreateEffect(ExecutionStateCreateEffect effect);
  void visitExecutionStateDeleteEffect(ExecutionStateDeleteEffect effect);
  void visitExecutionStateSetActingUnitEffect(ExecutionStateSetActingUnitEffect effect);
  void visitExecutionStateSetActingUnitDidActionEffect(ExecutionStateSetActingUnitDidActionEffect effect);
  void visitExecutionStateSetRemainingPreActingDetailsEffect(ExecutionStateSetRemainingPreActingDetailsEffect effect);
  void visitExecutionStateSetRemainingPostActingDetailsEffect(ExecutionStateSetRemainingPostActingDetailsEffect effect);
}

}
