using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianFloorTTCEffectVisitor {
  void visitObsidianFloorTTCCreateEffect(ObsidianFloorTTCCreateEffect effect);
  void visitObsidianFloorTTCDeleteEffect(ObsidianFloorTTCDeleteEffect effect);
}

}
