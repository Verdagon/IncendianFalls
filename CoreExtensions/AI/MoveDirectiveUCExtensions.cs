using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class MoveDirectiveUCExtensions {
    public static Atharia.Model.Void Destruct(
        this MoveDirectiveUC obj) {
      var thing = obj.path;
      obj.Delete();
      thing.Destruct();
      return new Atharia.Model.Void();
    }
  }
}
