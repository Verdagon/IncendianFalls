using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CommIncarnation : ICommEffectVisitor {
  public readonly ICommTemplate template;
  public readonly CommActionImmList actions;
  public readonly CommTextImmList texts;
  public CommIncarnation(
      ICommTemplate template,
      CommActionImmList actions,
      CommTextImmList texts) {
    this.template = template;
    this.actions = actions;
    this.texts = texts;
  }
  public CommIncarnation Copy() {
    return new CommIncarnation(
template,
actions,
texts    );
  }

  public void visitCommCreateEffect(CommCreateEffect e) {}
  public void visitCommDeleteEffect(CommDeleteEffect e) {}



  public void ApplyEffect(ICommEffect effect) { effect.visitICommEffect(this); }
}

}
