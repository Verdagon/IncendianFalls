using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelLinkTTCMutSetEffect {
  int id { get; }
  void visit(ILevelLinkTTCMutSetEffectVisitor visitor);
}

}
