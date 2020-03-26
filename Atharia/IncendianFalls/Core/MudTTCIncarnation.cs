using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MudTTCIncarnation : IMudTTCEffectVisitor {
  public MudTTCIncarnation(
) {
  }
  public MudTTCIncarnation Copy() {
    return new MudTTCIncarnation(
    );
  }

  public void visitMudTTCCreateEffect(MudTTCCreateEffect e) {}
  public void visitMudTTCDeleteEffect(MudTTCDeleteEffect e) {}

  public void ApplyEffect(IMudTTCEffect effect) { effect.visitIMudTTCEffect(this); }
}

}
