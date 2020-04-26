using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStairsTTCEffectVisitor {
  void visitDownStairsTTCCreateEffect(DownStairsTTCCreateEffect effect);
  void visitDownStairsTTCDeleteEffect(DownStairsTTCDeleteEffect effect);
}

}
