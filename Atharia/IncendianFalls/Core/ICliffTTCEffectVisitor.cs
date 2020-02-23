using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffTTCEffectVisitor {
  void visitCliffTTCCreateEffect(CliffTTCCreateEffect effect);
  void visitCliffTTCDeleteEffect(CliffTTCDeleteEffect effect);
}

}
