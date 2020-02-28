using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorStrongMutSetEffectVisitor {
  void visitArmorStrongMutSetCreateEffect(ArmorStrongMutSetCreateEffect effect);
  void visitArmorStrongMutSetDeleteEffect(ArmorStrongMutSetDeleteEffect effect);
  void visitArmorStrongMutSetAddEffect(ArmorStrongMutSetAddEffect effect);
  void visitArmorStrongMutSetRemoveEffect(ArmorStrongMutSetRemoveEffect effect);
}
         
}
