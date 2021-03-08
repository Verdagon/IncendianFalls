using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITemporaryCloneAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitITemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffectVisitor visitor);
}
       
}
