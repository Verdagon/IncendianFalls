using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodStrongMutSetEffectVisitor {
  void visitBlastRodStrongMutSetCreateEffect(BlastRodStrongMutSetCreateEffect effect);
  void visitBlastRodStrongMutSetDeleteEffect(BlastRodStrongMutSetDeleteEffect effect);
  void visitBlastRodStrongMutSetAddEffect(BlastRodStrongMutSetAddEffect effect);
  void visitBlastRodStrongMutSetRemoveEffect(BlastRodStrongMutSetRemoveEffect effect);
}
         
}
