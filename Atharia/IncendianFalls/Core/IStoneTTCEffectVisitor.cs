using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStoneTTCEffectVisitor {
  void visitStoneTTCCreateEffect(StoneTTCCreateEffect effect);
  void visitStoneTTCDeleteEffect(StoneTTCDeleteEffect effect);
}

}
