using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCWeakMutSetEffectVisitor {
  void visitDoomedUCWeakMutSetCreateEffect(DoomedUCWeakMutSetCreateEffect effect);
  void visitDoomedUCWeakMutSetDeleteEffect(DoomedUCWeakMutSetDeleteEffect effect);
  void visitDoomedUCWeakMutSetAddEffect(DoomedUCWeakMutSetAddEffect effect);
  void visitDoomedUCWeakMutSetRemoveEffect(DoomedUCWeakMutSetRemoveEffect effect);
}
         
}
