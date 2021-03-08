using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGuardAICapabilityUCEffectVisitor {
  void visitGuardAICapabilityUCCreateEffect(GuardAICapabilityUCCreateEffect effect);
  void visitGuardAICapabilityUCDeleteEffect(GuardAICapabilityUCDeleteEffect effect);
}

}
