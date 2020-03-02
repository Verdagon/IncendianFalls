using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGameEvent {
  string DStr();
  int GetDeterministicHashCode();
  void Visit(IGameEventVisitor visitor);
}

}
