using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGrassTTCMutSetEffect {
  int id { get; }
  void visit(IGrassTTCMutSetEffectVisitor visitor);
}

}
