using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffectVisitor visitor);
}

}
