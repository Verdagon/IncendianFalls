using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStairsTTCIncarnation : IDownStairsTTCEffectVisitor {
  public DownStairsTTCIncarnation(
) {
  }
  public DownStairsTTCIncarnation Copy() {
    return new DownStairsTTCIncarnation(
    );
  }

  public void visitDownStairsTTCCreateEffect(DownStairsTTCCreateEffect e) {}
  public void visitDownStairsTTCDeleteEffect(DownStairsTTCDeleteEffect e) {}

  public void ApplyEffect(IDownStairsTTCEffect effect) { effect.visitIDownStairsTTCEffect(this); }
}

}
