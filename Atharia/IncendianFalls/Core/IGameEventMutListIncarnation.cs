using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IGameEventMutListIncarnation {
  public readonly List<IGameEvent> list;

  public IGameEventMutListIncarnation(List<IGameEvent> list) {
    this.list = list;
  }

  public IGameEventMutListIncarnation Copy() {
    return new IGameEventMutListIncarnation(new List<IGameEvent>(list));
  }
}
         
}
