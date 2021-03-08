using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISpeedRingEffectVisitor {
  void visitSpeedRingCreateEffect(SpeedRingCreateEffect effect);
  void visitSpeedRingDeleteEffect(SpeedRingDeleteEffect effect);
}

}
