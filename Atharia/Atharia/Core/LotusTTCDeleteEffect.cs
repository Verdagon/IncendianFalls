using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LotusTTCDeleteEffect : ILotusTTCEffect {
  public readonly int id;
  public LotusTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ILotusTTCEffect.id => id;
  public void visitILotusTTCEffect(ILotusTTCEffectVisitor visitor) {
    visitor.visitLotusTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
