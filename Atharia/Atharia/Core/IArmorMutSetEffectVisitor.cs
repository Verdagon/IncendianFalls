using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorMutSetEffectVisitor {
  void visitArmorMutSetCreateEffect(ArmorMutSetCreateEffect effect);
  void visitArmorMutSetDeleteEffect(ArmorMutSetDeleteEffect effect);
  void visitArmorMutSetAddEffect(ArmorMutSetAddEffect effect);
  void visitArmorMutSetRemoveEffect(ArmorMutSetRemoveEffect effect);
}
         
}
