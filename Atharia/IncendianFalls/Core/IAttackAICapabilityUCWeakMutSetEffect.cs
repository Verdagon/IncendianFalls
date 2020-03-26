using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackAICapabilityUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor);
}

}
