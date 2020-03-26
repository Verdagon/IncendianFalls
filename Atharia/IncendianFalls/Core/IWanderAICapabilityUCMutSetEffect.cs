using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWanderAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffectVisitor visitor);
}

}
