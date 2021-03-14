using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LeafTTCIncarnation : ILeafTTCEffectVisitor {
  public LeafTTCIncarnation(
) {
  }
  public LeafTTCIncarnation Copy() {
    return new LeafTTCIncarnation(
    );
  }

  public void visitLeafTTCCreateEffect(LeafTTCCreateEffect e) {}
  public void visitLeafTTCDeleteEffect(LeafTTCDeleteEffect e) {}

  public void ApplyEffect(ILeafTTCEffect effect) { effect.visitILeafTTCEffect(this); }
}

}
