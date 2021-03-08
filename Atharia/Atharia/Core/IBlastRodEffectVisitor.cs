using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodEffectVisitor {
  void visitBlastRodCreateEffect(BlastRodCreateEffect effect);
  void visitBlastRodDeleteEffect(BlastRodDeleteEffect effect);
}

}
