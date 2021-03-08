using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FallsTTCIncarnation : IFallsTTCEffectVisitor {
  public FallsTTCIncarnation(
) {
  }
  public FallsTTCIncarnation Copy() {
    return new FallsTTCIncarnation(
    );
  }

  public void visitFallsTTCCreateEffect(FallsTTCCreateEffect e) {}
  public void visitFallsTTCDeleteEffect(FallsTTCDeleteEffect e) {}

  public void ApplyEffect(IFallsTTCEffect effect) { effect.visitIFallsTTCEffect(this); }
}

}
