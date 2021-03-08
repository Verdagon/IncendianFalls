using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneAICapabilityUCEffectVisitor {
  void visitTemporaryCloneAICapabilityUCCreateEffect(TemporaryCloneAICapabilityUCCreateEffect effect);
  void visitTemporaryCloneAICapabilityUCDeleteEffect(TemporaryCloneAICapabilityUCDeleteEffect effect);
  void visitTemporaryCloneAICapabilityUCSetChargesEffect(TemporaryCloneAICapabilityUCSetChargesEffect effect);
}

}
