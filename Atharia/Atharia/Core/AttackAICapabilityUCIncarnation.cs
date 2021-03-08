using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCIncarnation : IAttackAICapabilityUCEffectVisitor {
  public  int killDirective;
  public AttackAICapabilityUCIncarnation(
      int killDirective) {
    this.killDirective = killDirective;
  }
  public AttackAICapabilityUCIncarnation Copy() {
    return new AttackAICapabilityUCIncarnation(
killDirective    );
  }

  public void visitAttackAICapabilityUCCreateEffect(AttackAICapabilityUCCreateEffect e) {}
  public void visitAttackAICapabilityUCDeleteEffect(AttackAICapabilityUCDeleteEffect e) {}
public void visitAttackAICapabilityUCSetKillDirectiveEffect(AttackAICapabilityUCSetKillDirectiveEffect e) { this.killDirective = e.newValue; }
  public void ApplyEffect(IAttackAICapabilityUCEffect effect) { effect.visitIAttackAICapabilityUCEffect(this); }
}

}
