using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILotusTTCEffectVisitor {
  void visitLotusTTCCreateEffect(LotusTTCCreateEffect effect);
  void visitLotusTTCDeleteEffect(LotusTTCDeleteEffect effect);
}

}
