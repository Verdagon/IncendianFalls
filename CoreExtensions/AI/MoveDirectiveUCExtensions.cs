using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class MoveDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this MoveDirectiveUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
