using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeCloneAICapabilityUCEffectVisitor {
  void visitTimeCloneAICapabilityUCCreateEffect(TimeCloneAICapabilityUCCreateEffect effect);
  void visitTimeCloneAICapabilityUCDeleteEffect(TimeCloneAICapabilityUCDeleteEffect effect);
}

}
