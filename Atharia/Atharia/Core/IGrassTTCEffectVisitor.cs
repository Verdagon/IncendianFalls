using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGrassTTCEffectVisitor {
  void visitGrassTTCCreateEffect(GrassTTCCreateEffect effect);
  void visitGrassTTCDeleteEffect(GrassTTCDeleteEffect effect);
}

}
