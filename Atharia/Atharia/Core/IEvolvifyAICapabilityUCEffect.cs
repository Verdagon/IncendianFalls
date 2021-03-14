using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEvolvifyAICapabilityUCEffect : IEffect {
  int id { get; }
  void visitIEvolvifyAICapabilityUCEffect(IEvolvifyAICapabilityUCEffectVisitor visitor);
}
       
}
