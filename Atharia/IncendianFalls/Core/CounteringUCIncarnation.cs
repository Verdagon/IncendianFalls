using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCIncarnation : ICounteringUCEffectVisitor {
  public CounteringUCIncarnation(
) {
  }
  public CounteringUCIncarnation Copy() {
    return new CounteringUCIncarnation(
    );
  }

  public void visitCounteringUCCreateEffect(CounteringUCCreateEffect e) {}
  public void visitCounteringUCDeleteEffect(CounteringUCDeleteEffect e) {}

  public void ApplyEffect(ICounteringUCEffect effect) { effect.visitICounteringUCEffect(this); }
}

}
