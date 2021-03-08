using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEmberDeepLevelLinkerTTCEffectVisitor {
  void visitEmberDeepLevelLinkerTTCCreateEffect(EmberDeepLevelLinkerTTCCreateEffect effect);
  void visitEmberDeepLevelLinkerTTCDeleteEffect(EmberDeepLevelLinkerTTCDeleteEffect effect);
}

}
