using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianTTCEffectVisitor {
  void visitObsidianTTCCreateEffect(ObsidianTTCCreateEffect effect);
  void visitObsidianTTCDeleteEffect(ObsidianTTCDeleteEffect effect);
}

}
