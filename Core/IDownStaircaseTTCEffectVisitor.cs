using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTTCEffectVisitor {
  void visitDownStaircaseTTCCreateEffect(DownStaircaseTTCCreateEffect effect);
  void visitDownStaircaseTTCDeleteEffect(DownStaircaseTTCDeleteEffect effect);
}

}
