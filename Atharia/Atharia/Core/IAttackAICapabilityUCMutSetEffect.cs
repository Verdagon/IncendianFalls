using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffectVisitor visitor);
}

}
