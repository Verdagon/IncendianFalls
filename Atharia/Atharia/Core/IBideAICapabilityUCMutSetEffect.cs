using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBideAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffectVisitor visitor);
}

}
