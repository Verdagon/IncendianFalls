using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTTCEffectVisitor {
  void visitDecorativeTTCCreateEffect(DecorativeTTCCreateEffect effect);
  void visitDecorativeTTCDeleteEffect(DecorativeTTCDeleteEffect effect);
}

}
