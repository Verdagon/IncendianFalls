using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISummonAICapabilityUCEffect {
  int id { get; }
  void visit(ISummonAICapabilityUCEffectVisitor visitor);
}
       
}
