using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITimeCloneAICapabilityUCEffect {
  int id { get; }
  void visit(ITimeCloneAICapabilityUCEffectVisitor visitor);
}
       
}
