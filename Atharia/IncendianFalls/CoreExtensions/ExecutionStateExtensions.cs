using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class ExecutionStateExtensions {
    public static string SummaryStr(this ExecutionState obj) {
      string str = "ExecutionState(actingUnit=";

      if (obj.actingUnit.Exists()) {
        str += obj.actingUnit.id;
      } else {
        str += "null";
      }

      str += " actingUnitDidAction=" + obj.actingUnitDidAction;

      if (obj.remainingPreActingUnitComponents.Exists()) {
        string preActingsStr = "";
        foreach (var thing in obj.remainingPreActingUnitComponents) {
          if (preActingsStr == "") {
            preActingsStr += ", ";
          }
          if (thing.Exists()) {
            preActingsStr += thing.id;
          } else {
            preActingsStr += "null";
          }
        }
        str += " remainingPreActingUnitComponents=[" + preActingsStr + "]";
      } else {
        str += " remainingPreActingUnitComponents=null";
      }

      if (obj.remainingPostActingUnitComponents.Exists()) {
        string postActingsStr = "";
        foreach (var thing in obj.remainingPreActingUnitComponents) {
          if (postActingsStr == "") {
            postActingsStr += ", ";
          }
          if (thing.Exists()) {
            postActingsStr += thing.id;
          } else {
            postActingsStr += "null";
          }
        }
        str += " remainingPostActingUnitComponents=[" + postActingsStr + "]";
      } else {
        str += " remainingPostActingUnitComponents=null";
      }

      str += ")";
      return str;
    }
  }
}
