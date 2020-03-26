using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SorcerousUCIncarnation : ISorcerousUCEffectVisitor {
  public  int mp;
  public  int maxMp;
  public SorcerousUCIncarnation(
      int mp,
      int maxMp) {
    this.mp = mp;
    this.maxMp = maxMp;
  }
  public SorcerousUCIncarnation Copy() {
    return new SorcerousUCIncarnation(
mp,
maxMp    );
  }

  public void visitSorcerousUCCreateEffect(SorcerousUCCreateEffect e) {}
  public void visitSorcerousUCDeleteEffect(SorcerousUCDeleteEffect e) {}
public void visitSorcerousUCSetMpEffect(SorcerousUCSetMpEffect e) { this.mp = e.newValue; }
public void visitSorcerousUCSetMaxMpEffect(SorcerousUCSetMaxMpEffect e) { this.maxMp = e.newValue; }
  public void ApplyEffect(ISorcerousUCEffect effect) { effect.visitISorcerousUCEffect(this); }
}

}
