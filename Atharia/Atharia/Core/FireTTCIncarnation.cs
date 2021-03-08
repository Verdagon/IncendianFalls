using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireTTCIncarnation : IFireTTCEffectVisitor {
  public FireTTCIncarnation(
) {
  }
  public FireTTCIncarnation Copy() {
    return new FireTTCIncarnation(
    );
  }

  public void visitFireTTCCreateEffect(FireTTCCreateEffect e) {}
  public void visitFireTTCDeleteEffect(FireTTCDeleteEffect e) {}

  public void ApplyEffect(IFireTTCEffect effect) { effect.visitIFireTTCEffect(this); }
}

}
