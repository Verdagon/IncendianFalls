using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitITemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffectVisitor visitor);
}

}
