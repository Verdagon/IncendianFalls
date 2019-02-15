using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IRandEffectVisitor {
  void visitRandCreateEffect(RandCreateEffect effect);
  void visitRandDeleteEffect(RandDeleteEffect effect);
  void visitRandSetRandEffect(RandSetRandEffect effect);
}

}
