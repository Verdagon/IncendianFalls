using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPreActingUCWeakMutBunchEffectVisitor {
  void visitIPreActingUCWeakMutBunchCreateEffect(IPreActingUCWeakMutBunchCreateEffect effect);
  void visitIPreActingUCWeakMutBunchDeleteEffect(IPreActingUCWeakMutBunchDeleteEffect effect);
}

}
