using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeFeatureEffectVisitor {
  void visitDecorativeFeatureCreateEffect(DecorativeFeatureCreateEffect effect);
  void visitDecorativeFeatureDeleteEffect(DecorativeFeatureDeleteEffect effect);
}

}
