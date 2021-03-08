using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIItemStrongMutBunchObserver {
  void OnIItemStrongMutBunchAdd(int id);
  void OnIItemStrongMutBunchRemove(int id);
}

}
