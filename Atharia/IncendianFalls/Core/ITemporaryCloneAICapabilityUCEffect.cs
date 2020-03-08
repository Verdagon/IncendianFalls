using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITemporaryCloneAICapabilityUCEffect {
  int id { get; }
  void visit(ITemporaryCloneAICapabilityUCEffectVisitor visitor);
}
       
}
