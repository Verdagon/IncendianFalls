using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveWallTTCMutSetEffectVisitor {
  void visitCaveWallTTCMutSetCreateEffect(CaveWallTTCMutSetCreateEffect effect);
  void visitCaveWallTTCMutSetDeleteEffect(CaveWallTTCMutSetDeleteEffect effect);
  void visitCaveWallTTCMutSetAddEffect(CaveWallTTCMutSetAddEffect effect);
  void visitCaveWallTTCMutSetRemoveEffect(CaveWallTTCMutSetRemoveEffect effect);
}
         
}
