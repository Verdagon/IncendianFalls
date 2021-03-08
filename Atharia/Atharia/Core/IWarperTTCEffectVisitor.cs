using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWarperTTCEffectVisitor {
  void visitWarperTTCCreateEffect(WarperTTCCreateEffect effect);
  void visitWarperTTCDeleteEffect(WarperTTCDeleteEffect effect);
}

}
