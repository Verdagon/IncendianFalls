using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveMutSetEffectVisitor {
  void visitGlaiveMutSetCreateEffect(GlaiveMutSetCreateEffect effect);
  void visitGlaiveMutSetDeleteEffect(GlaiveMutSetDeleteEffect effect);
  void visitGlaiveMutSetAddEffect(GlaiveMutSetAddEffect effect);
  void visitGlaiveMutSetRemoveEffect(GlaiveMutSetRemoveEffect effect);
}
         
}
