using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPostActingUCMutBunchEffectVisitor {
  void visitIPostActingUCMutBunchCreateEffect(IPostActingUCMutBunchCreateEffect effect);
  void visitIPostActingUCMutBunchDeleteEffect(IPostActingUCMutBunchDeleteEffect effect);
}

}
