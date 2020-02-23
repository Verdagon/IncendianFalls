using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownstairsTTCEffectVisitor {
  void visitDownstairsTTCCreateEffect(DownstairsTTCCreateEffect effect);
  void visitDownstairsTTCDeleteEffect(DownstairsTTCDeleteEffect effect);
}

}
