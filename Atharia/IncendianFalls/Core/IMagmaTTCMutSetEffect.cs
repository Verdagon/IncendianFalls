using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMagmaTTCMutSetEffect {
  int id { get; }
  void visit(IMagmaTTCMutSetEffectVisitor visitor);
}

}
