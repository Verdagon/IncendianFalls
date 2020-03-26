using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitISummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffectVisitor visitor);
}

}
