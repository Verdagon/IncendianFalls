using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GrassTTCIncarnation : IGrassTTCEffectVisitor {
  public GrassTTCIncarnation(
) {
  }
  public GrassTTCIncarnation Copy() {
    return new GrassTTCIncarnation(
    );
  }

  public void visitGrassTTCCreateEffect(GrassTTCCreateEffect e) {}
  public void visitGrassTTCDeleteEffect(GrassTTCDeleteEffect e) {}

  public void ApplyEffect(IGrassTTCEffect effect) { effect.visitIGrassTTCEffect(this); }
}

}
