using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUCEffectVisitor {
  void visitShieldingUCCreateEffect(ShieldingUCCreateEffect effect);
  void visitShieldingUCDeleteEffect(ShieldingUCDeleteEffect effect);
}

}
