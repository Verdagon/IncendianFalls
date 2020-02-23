using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKillDirectiveEffectVisitor {
  void visitKillDirectiveCreateEffect(KillDirectiveCreateEffect effect);
  void visitKillDirectiveDeleteEffect(KillDirectiveDeleteEffect effect);
}

}
