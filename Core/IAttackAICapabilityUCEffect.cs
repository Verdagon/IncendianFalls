using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackAICapabilityUCEffect {
  int id { get; }
  void visit(IAttackAICapabilityUCEffectVisitor visitor);
}
       
}
