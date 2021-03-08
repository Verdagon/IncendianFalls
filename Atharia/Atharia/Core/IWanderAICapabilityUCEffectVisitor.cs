using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWanderAICapabilityUCEffectVisitor {
  void visitWanderAICapabilityUCCreateEffect(WanderAICapabilityUCCreateEffect effect);
  void visitWanderAICapabilityUCDeleteEffect(WanderAICapabilityUCDeleteEffect effect);
}

}
