using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IRequestMutListIncarnation {
  public readonly List<IRequest> list;

  public IRequestMutListIncarnation(List<IRequest> list) {
    this.list = list;
  }

  public IRequestMutListIncarnation Copy() {
    return new IRequestMutListIncarnation(new List<IRequest>(list));
  }
}
         
}
