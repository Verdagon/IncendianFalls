using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LotusTTCIncarnation : ILotusTTCEffectVisitor {
  public LotusTTCIncarnation(
) {
  }
  public LotusTTCIncarnation Copy() {
    return new LotusTTCIncarnation(
    );
  }

  public void visitLotusTTCCreateEffect(LotusTTCCreateEffect e) {}
  public void visitLotusTTCDeleteEffect(LotusTTCDeleteEffect e) {}

  public void ApplyEffect(ILotusTTCEffect effect) { effect.visitILotusTTCEffect(this); }
}

}
