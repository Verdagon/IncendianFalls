using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFloorTTCEffectVisitor {
  void visitFloorTTCCreateEffect(FloorTTCCreateEffect effect);
  void visitFloorTTCDeleteEffect(FloorTTCDeleteEffect effect);
}

}
