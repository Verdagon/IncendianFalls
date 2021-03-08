using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveWallTTCEffectVisitor {
  void visitCaveWallTTCCreateEffect(CaveWallTTCCreateEffect effect);
  void visitCaveWallTTCDeleteEffect(CaveWallTTCDeleteEffect effect);
}

}
