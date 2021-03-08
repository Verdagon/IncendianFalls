using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGuardAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIGuardAICapabilityUCEffect(IGuardAICapabilityUCEffectVisitor visitor);
}
       
}
