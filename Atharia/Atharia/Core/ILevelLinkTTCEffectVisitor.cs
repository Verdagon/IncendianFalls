using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelLinkTTCEffectVisitor {
  void visitLevelLinkTTCCreateEffect(LevelLinkTTCCreateEffect effect);
  void visitLevelLinkTTCDeleteEffect(LevelLinkTTCDeleteEffect effect);
}

}
