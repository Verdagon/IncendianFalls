using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIAttackAICapabilityUCEffect(IAttackAICapabilityUCEffectVisitor visitor);
}
       
}
