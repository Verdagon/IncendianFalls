using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWanderAICapabilityUCEffect {
  int id { get; }
  void visit(IWanderAICapabilityUCEffectVisitor visitor);
}
       
}
