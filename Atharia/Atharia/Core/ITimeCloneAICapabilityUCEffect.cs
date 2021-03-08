using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITimeCloneAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitITimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffectVisitor visitor);
}
       
}
