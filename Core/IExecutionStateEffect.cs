using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IExecutionStateEffect {
  int id { get; }
  void visit(IExecutionStateEffectVisitor visitor);
}
       
}
