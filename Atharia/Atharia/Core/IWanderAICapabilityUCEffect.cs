using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWanderAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIWanderAICapabilityUCEffect(IWanderAICapabilityUCEffectVisitor visitor);
}
       
}
