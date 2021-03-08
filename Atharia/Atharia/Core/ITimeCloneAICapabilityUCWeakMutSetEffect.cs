using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeCloneAICapabilityUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitITimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffectVisitor visitor);
}

}
