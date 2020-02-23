using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpstairsTTCMutSetEffect {
  int id { get; }
  void visit(IUpstairsTTCMutSetEffectVisitor visitor);
}

}
