using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBideAICapabilityUCEffect {
  int id { get; }
  void visit(IBideAICapabilityUCEffectVisitor visitor);
}
       
}
