using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyAICapabilityUCEffectVisitor {
  void visitEvolvifyAICapabilityUCCreateEffect(EvolvifyAICapabilityUCCreateEffect effect);
  void visitEvolvifyAICapabilityUCDeleteEffect(EvolvifyAICapabilityUCDeleteEffect effect);
}

}
