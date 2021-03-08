using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStairsTTCIncarnation : IUpStairsTTCEffectVisitor {
  public UpStairsTTCIncarnation(
) {
  }
  public UpStairsTTCIncarnation Copy() {
    return new UpStairsTTCIncarnation(
    );
  }

  public void visitUpStairsTTCCreateEffect(UpStairsTTCCreateEffect e) {}
  public void visitUpStairsTTCDeleteEffect(UpStairsTTCDeleteEffect e) {}

  public void ApplyEffect(IUpStairsTTCEffect effect) { effect.visitIUpStairsTTCEffect(this); }
}

}
