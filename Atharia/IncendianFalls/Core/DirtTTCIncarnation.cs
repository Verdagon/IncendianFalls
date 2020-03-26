using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DirtTTCIncarnation : IDirtTTCEffectVisitor {
  public DirtTTCIncarnation(
) {
  }
  public DirtTTCIncarnation Copy() {
    return new DirtTTCIncarnation(
    );
  }

  public void visitDirtTTCCreateEffect(DirtTTCCreateEffect e) {}
  public void visitDirtTTCDeleteEffect(DirtTTCDeleteEffect e) {}

  public void ApplyEffect(IDirtTTCEffect effect) { effect.visitIDirtTTCEffect(this); }
}

}
