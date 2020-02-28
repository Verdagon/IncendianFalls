using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveStrongMutSetEffectVisitor {
  void visitGlaiveStrongMutSetCreateEffect(GlaiveStrongMutSetCreateEffect effect);
  void visitGlaiveStrongMutSetDeleteEffect(GlaiveStrongMutSetDeleteEffect effect);
  void visitGlaiveStrongMutSetAddEffect(GlaiveStrongMutSetAddEffect effect);
  void visitGlaiveStrongMutSetRemoveEffect(GlaiveStrongMutSetRemoveEffect effect);
}
         
}
