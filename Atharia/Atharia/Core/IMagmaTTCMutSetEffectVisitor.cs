using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMagmaTTCMutSetEffectVisitor {
  void visitMagmaTTCMutSetCreateEffect(MagmaTTCMutSetCreateEffect effect);
  void visitMagmaTTCMutSetDeleteEffect(MagmaTTCMutSetDeleteEffect effect);
  void visitMagmaTTCMutSetAddEffect(MagmaTTCMutSetAddEffect effect);
  void visitMagmaTTCMutSetRemoveEffect(MagmaTTCMutSetRemoveEffect effect);
}
         
}
