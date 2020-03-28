using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class CommExtensions {
    public static Atharia.Model.Void Destruct(
        this Comm obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }
  }
}
