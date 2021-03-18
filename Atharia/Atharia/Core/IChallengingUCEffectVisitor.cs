using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IChallengingUCEffectVisitor {
  void visitChallengingUCCreateEffect(ChallengingUCCreateEffect effect);
  void visitChallengingUCDeleteEffect(ChallengingUCDeleteEffect effect);
}

}
