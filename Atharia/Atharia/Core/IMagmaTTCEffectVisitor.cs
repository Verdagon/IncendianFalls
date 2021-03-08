using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMagmaTTCEffectVisitor {
  void visitMagmaTTCCreateEffect(MagmaTTCCreateEffect effect);
  void visitMagmaTTCDeleteEffect(MagmaTTCDeleteEffect effect);
}

}
