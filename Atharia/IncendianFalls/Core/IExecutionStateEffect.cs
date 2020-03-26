using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IExecutionStateEffect : IEffect {
  int id { get; }
  void visitIExecutionStateEffect(IExecutionStateEffectVisitor visitor);
}
       
}
