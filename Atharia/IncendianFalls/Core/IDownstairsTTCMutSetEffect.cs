using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownstairsTTCMutSetEffect {
  int id { get; }
  void visit(IDownstairsTTCMutSetEffectVisitor visitor);
}

}
