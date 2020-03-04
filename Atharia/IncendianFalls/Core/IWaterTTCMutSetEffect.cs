using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWaterTTCMutSetEffect {
  int id { get; }
  void visit(IWaterTTCMutSetEffectVisitor visitor);
}

}
