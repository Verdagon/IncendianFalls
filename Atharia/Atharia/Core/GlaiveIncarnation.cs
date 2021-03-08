using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveIncarnation : IGlaiveEffectVisitor {
  public GlaiveIncarnation(
) {
  }
  public GlaiveIncarnation Copy() {
    return new GlaiveIncarnation(
    );
  }

  public void visitGlaiveCreateEffect(GlaiveCreateEffect e) {}
  public void visitGlaiveDeleteEffect(GlaiveDeleteEffect e) {}

  public void ApplyEffect(IGlaiveEffect effect) { effect.visitIGlaiveEffect(this); }
}

}
