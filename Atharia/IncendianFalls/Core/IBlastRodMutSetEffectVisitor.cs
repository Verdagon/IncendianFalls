using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodMutSetEffectVisitor {
  void visitBlastRodMutSetCreateEffect(BlastRodMutSetCreateEffect effect);
  void visitBlastRodMutSetDeleteEffect(BlastRodMutSetDeleteEffect effect);
  void visitBlastRodMutSetAddEffect(BlastRodMutSetAddEffect effect);
  void visitBlastRodMutSetRemoveEffect(BlastRodMutSetRemoveEffect effect);
}
         
}
