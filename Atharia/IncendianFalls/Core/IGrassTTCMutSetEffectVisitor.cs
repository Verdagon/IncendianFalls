using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGrassTTCMutSetEffectVisitor {
  void visitGrassTTCMutSetCreateEffect(GrassTTCMutSetCreateEffect effect);
  void visitGrassTTCMutSetDeleteEffect(GrassTTCMutSetDeleteEffect effect);
  void visitGrassTTCMutSetAddEffect(GrassTTCMutSetAddEffect effect);
  void visitGrassTTCMutSetRemoveEffect(GrassTTCMutSetRemoveEffect effect);
}
         
}
