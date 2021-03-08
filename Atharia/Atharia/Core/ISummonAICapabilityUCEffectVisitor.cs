using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonAICapabilityUCEffectVisitor {
  void visitSummonAICapabilityUCCreateEffect(SummonAICapabilityUCCreateEffect effect);
  void visitSummonAICapabilityUCDeleteEffect(SummonAICapabilityUCDeleteEffect effect);
  void visitSummonAICapabilityUCSetChargesEffect(SummonAICapabilityUCSetChargesEffect effect);
}

}
