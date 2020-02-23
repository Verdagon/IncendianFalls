using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffLandingTTCEffectVisitor {
  void visitCliffLandingTTCCreateEffect(CliffLandingTTCCreateEffect effect);
  void visitCliffLandingTTCDeleteEffect(CliffLandingTTCDeleteEffect effect);
}

}
