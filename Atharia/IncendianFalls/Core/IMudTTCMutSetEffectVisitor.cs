using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMudTTCMutSetEffectVisitor {
  void visitMudTTCMutSetCreateEffect(MudTTCMutSetCreateEffect effect);
  void visitMudTTCMutSetDeleteEffect(MudTTCMutSetDeleteEffect effect);
  void visitMudTTCMutSetAddEffect(MudTTCMutSetAddEffect effect);
  void visitMudTTCMutSetRemoveEffect(MudTTCMutSetRemoveEffect effect);
}
         
}
