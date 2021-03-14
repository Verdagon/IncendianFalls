using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LotusTTCMutSetDeleteEffect : ILotusTTCMutSetEffect {
  public readonly int id;
  public LotusTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILotusTTCMutSetEffect.id => id;
  public void visitILotusTTCMutSetEffect(ILotusTTCMutSetEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
