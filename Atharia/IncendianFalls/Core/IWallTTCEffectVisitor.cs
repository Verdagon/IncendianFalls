using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWallTTCEffectVisitor {
  void visitWallTTCCreateEffect(WallTTCCreateEffect effect);
  void visitWallTTCDeleteEffect(WallTTCDeleteEffect effect);
}

}
