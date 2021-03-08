using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveTTCEffectVisitor {
  void visitCaveTTCCreateEffect(CaveTTCCreateEffect effect);
  void visitCaveTTCDeleteEffect(CaveTTCDeleteEffect effect);
}

}
