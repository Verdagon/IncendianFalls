using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIItemMutBunchObserver {
  void OnIItemMutBunchAdd(int id);
  void OnIItemMutBunchRemove(int id);
}

}
