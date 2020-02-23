using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpstairsTTCEffectVisitor {
  void visitUpstairsTTCCreateEffect(UpstairsTTCCreateEffect effect);
  void visitUpstairsTTCDeleteEffect(UpstairsTTCDeleteEffect effect);
}

}
