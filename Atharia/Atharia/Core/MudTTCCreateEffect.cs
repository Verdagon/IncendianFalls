using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MudTTCCreateEffect : IMudTTCEffect {
  public readonly int id;
  public readonly MudTTCIncarnation incarnation;
  public MudTTCCreateEffect(int id, MudTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMudTTCEffect.id => id;
  public void visitIMudTTCEffect(IMudTTCEffectVisitor visitor) {
    visitor.visitMudTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMudTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
