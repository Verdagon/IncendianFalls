using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefendingDetailEffectVisitor {
  void visitDefendingDetailCreateEffect(DefendingDetailCreateEffect effect);
  void visitDefendingDetailDeleteEffect(DefendingDetailDeleteEffect effect);
  void visitDefendingDetailSetPowerEffect(DefendingDetailSetPowerEffect effect);
}

}
