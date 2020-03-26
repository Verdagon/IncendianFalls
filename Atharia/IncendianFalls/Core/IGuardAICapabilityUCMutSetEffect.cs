using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGuardAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffectVisitor visitor);
}

}
