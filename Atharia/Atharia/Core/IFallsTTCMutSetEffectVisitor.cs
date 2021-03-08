using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFallsTTCMutSetEffectVisitor {
  void visitFallsTTCMutSetCreateEffect(FallsTTCMutSetCreateEffect effect);
  void visitFallsTTCMutSetDeleteEffect(FallsTTCMutSetDeleteEffect effect);
  void visitFallsTTCMutSetAddEffect(FallsTTCMutSetAddEffect effect);
  void visitFallsTTCMutSetRemoveEffect(FallsTTCMutSetRemoveEffect effect);
}
         
}
