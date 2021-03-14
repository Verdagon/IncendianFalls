using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlazeRodEffectVisitor {
  void visitBlazeRodCreateEffect(BlazeRodCreateEffect effect);
  void visitBlazeRodDeleteEffect(BlazeRodDeleteEffect effect);
}

}
