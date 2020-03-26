using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeCloneAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitITimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffectVisitor visitor);
}

}
