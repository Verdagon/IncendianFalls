using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaArcanaLevelLinkerTTCEffectVisitor {
  void visitRavaArcanaLevelLinkerTTCCreateEffect(RavaArcanaLevelLinkerTTCCreateEffect effect);
  void visitRavaArcanaLevelLinkerTTCDeleteEffect(RavaArcanaLevelLinkerTTCDeleteEffect effect);
}

}
