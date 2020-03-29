using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class RandIncarnation : IRandEffectVisitor {
  public  int rand;
  public RandIncarnation(
      int rand) {
    this.rand = rand;
  }
  public RandIncarnation Copy() {
    return new RandIncarnation(
rand    );
  }

  public void visitRandCreateEffect(RandCreateEffect e) {}
  public void visitRandDeleteEffect(RandDeleteEffect e) {}
public void visitRandSetRandEffect(RandSetRandEffect e) { this.rand = e.newValue; }
  public void ApplyEffect(IRandEffect effect) { effect.visitIRandEffect(this); }
}

}
