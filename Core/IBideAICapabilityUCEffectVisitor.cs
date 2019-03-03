using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBideAICapabilityUCEffectVisitor {
  void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect effect);
  void visitBideAICapabilityUCDeleteEffect(BideAICapabilityUCDeleteEffect effect);
}

}
