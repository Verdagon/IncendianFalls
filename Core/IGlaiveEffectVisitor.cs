using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveEffectVisitor {
  void visitGlaiveCreateEffect(GlaiveCreateEffect effect);
  void visitGlaiveDeleteEffect(GlaiveDeleteEffect effect);
}

}
