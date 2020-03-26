using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISummonAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitISummonAICapabilityUCEffect(ISummonAICapabilityUCEffectVisitor visitor);
}
       
}
