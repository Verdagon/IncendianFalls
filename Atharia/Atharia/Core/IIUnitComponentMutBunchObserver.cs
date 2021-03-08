using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIUnitComponentMutBunchObserver {
  void OnIUnitComponentMutBunchAdd(int id);
  void OnIUnitComponentMutBunchRemove(int id);
}

}
