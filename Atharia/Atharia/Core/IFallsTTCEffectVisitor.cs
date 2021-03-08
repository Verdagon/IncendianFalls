using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFallsTTCEffectVisitor {
  void visitFallsTTCCreateEffect(FallsTTCCreateEffect effect);
  void visitFallsTTCDeleteEffect(FallsTTCDeleteEffect effect);
}

}
