using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseFeatureEffectVisitor {
  void visitUpStaircaseFeatureCreateEffect(UpStaircaseFeatureCreateEffect effect);
  void visitUpStaircaseFeatureDeleteEffect(UpStaircaseFeatureDeleteEffect effect);
}

}
