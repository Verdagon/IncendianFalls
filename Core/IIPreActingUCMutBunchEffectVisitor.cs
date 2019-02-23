using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPreActingUCMutBunchEffectVisitor {
  void visitIPreActingUCMutBunchCreateEffect(IPreActingUCMutBunchCreateEffect effect);
  void visitIPreActingUCMutBunchDeleteEffect(IPreActingUCMutBunchDeleteEffect effect);
}

}
