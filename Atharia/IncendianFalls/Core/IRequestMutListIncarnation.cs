using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IRequestMutListIncarnation {
  public readonly List<IRequest> elements;

  public IRequestMutListIncarnation(List<IRequest> elements) {
    this.elements = elements;
  }

  public IRequestMutListIncarnation Copy() {
    return new IRequestMutListIncarnation(new List<IRequest>(elements));
  }
}
         
}
