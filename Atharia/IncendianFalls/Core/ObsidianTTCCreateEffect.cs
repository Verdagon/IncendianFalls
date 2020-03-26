using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianTTCCreateEffect : IObsidianTTCEffect {
  public readonly int id;
  public readonly ObsidianTTCIncarnation incarnation;
  public ObsidianTTCCreateEffect(int id, ObsidianTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IObsidianTTCEffect.id => id;
  public void visitIObsidianTTCEffect(IObsidianTTCEffectVisitor visitor) {
    visitor.visitObsidianTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianTTCEffect(this);
  }
}

}
