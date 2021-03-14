using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvolvifyAICapabilityUCDeleteEffect : IEvolvifyAICapabilityUCEffect {
  public readonly int id;
  public EvolvifyAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IEvolvifyAICapabilityUCEffect.id => id;
  public void visitIEvolvifyAICapabilityUCEffect(IEvolvifyAICapabilityUCEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
