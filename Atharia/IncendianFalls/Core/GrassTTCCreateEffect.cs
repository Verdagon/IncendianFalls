using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GrassTTCCreateEffect : IGrassTTCEffect {
  public readonly int id;
  public readonly GrassTTCIncarnation incarnation;
  public GrassTTCCreateEffect(int id, GrassTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGrassTTCEffect.id => id;
  public void visitIGrassTTCEffect(IGrassTTCEffectVisitor visitor) {
    visitor.visitGrassTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGrassTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
