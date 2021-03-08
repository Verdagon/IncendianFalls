using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWallTTCMutSetEffectVisitor {
  void visitWallTTCMutSetCreateEffect(WallTTCMutSetCreateEffect effect);
  void visitWallTTCMutSetDeleteEffect(WallTTCMutSetDeleteEffect effect);
  void visitWallTTCMutSetAddEffect(WallTTCMutSetAddEffect effect);
  void visitWallTTCMutSetRemoveEffect(WallTTCMutSetRemoveEffect effect);
}
         
}
