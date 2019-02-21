using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUnitComponentEffectVisitor {
  void visitShieldingUnitComponentCreateEffect(ShieldingUnitComponentCreateEffect effect);
  void visitShieldingUnitComponentDeleteEffect(ShieldingUnitComponentDeleteEffect effect);
  void visitShieldingUnitComponentSetPowerEffect(ShieldingUnitComponentSetPowerEffect effect);
}

}
