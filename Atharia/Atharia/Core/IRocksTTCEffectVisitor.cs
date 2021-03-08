using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRocksTTCEffectVisitor {
  void visitRocksTTCCreateEffect(RocksTTCCreateEffect effect);
  void visitRocksTTCDeleteEffect(RocksTTCDeleteEffect effect);
}

}
