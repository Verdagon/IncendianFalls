using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTTCEffectVisitor {
  void visitUpStaircaseTTCCreateEffect(UpStaircaseTTCCreateEffect effect);
  void visitUpStaircaseTTCDeleteEffect(UpStaircaseTTCDeleteEffect effect);
}

}
