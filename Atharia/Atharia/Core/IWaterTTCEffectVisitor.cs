using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWaterTTCEffectVisitor {
  void visitWaterTTCCreateEffect(WaterTTCCreateEffect effect);
  void visitWaterTTCDeleteEffect(WaterTTCDeleteEffect effect);
}

}
