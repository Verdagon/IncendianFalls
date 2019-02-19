using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseFeatureEffectVisitor {
  void visitDownStaircaseFeatureCreateEffect(DownStaircaseFeatureCreateEffect effect);
  void visitDownStaircaseFeatureDeleteEffect(DownStaircaseFeatureDeleteEffect effect);
}

}
