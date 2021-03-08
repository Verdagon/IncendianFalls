using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaNestTTCEffectVisitor {
  void visitRavaNestTTCCreateEffect(RavaNestTTCCreateEffect effect);
  void visitRavaNestTTCDeleteEffect(RavaNestTTCDeleteEffect effect);
}

}
