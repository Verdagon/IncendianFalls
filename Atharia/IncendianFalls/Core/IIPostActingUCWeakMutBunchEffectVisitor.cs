using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPostActingUCWeakMutBunchEffectVisitor {
  void visitIPostActingUCWeakMutBunchCreateEffect(IPostActingUCWeakMutBunchCreateEffect effect);
  void visitIPostActingUCWeakMutBunchDeleteEffect(IPostActingUCWeakMutBunchDeleteEffect effect);
}

}
