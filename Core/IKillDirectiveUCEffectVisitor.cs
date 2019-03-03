using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKillDirectiveUCEffectVisitor {
  void visitKillDirectiveUCCreateEffect(KillDirectiveUCCreateEffect effect);
  void visitKillDirectiveUCDeleteEffect(KillDirectiveUCDeleteEffect effect);
}

}
