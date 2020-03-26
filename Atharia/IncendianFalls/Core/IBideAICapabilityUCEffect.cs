using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBideAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIBideAICapabilityUCEffect(IBideAICapabilityUCEffectVisitor visitor);
}
       
}
