using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCMutSetEffectVisitor {
  void visitDoomedUCMutSetCreateEffect(DoomedUCMutSetCreateEffect effect);
  void visitDoomedUCMutSetDeleteEffect(DoomedUCMutSetDeleteEffect effect);
  void visitDoomedUCMutSetAddEffect(DoomedUCMutSetAddEffect effect);
  void visitDoomedUCMutSetRemoveEffect(DoomedUCMutSetRemoveEffect effect);
}
         
}
