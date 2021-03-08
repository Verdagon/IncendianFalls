using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public interface IRandEffectVisitor {
  void visitRandCreateEffect(RandCreateEffect effect);
  void visitRandDeleteEffect(RandDeleteEffect effect);
  void visitRandSetRandEffect(RandSetRandEffect effect);
}

}
