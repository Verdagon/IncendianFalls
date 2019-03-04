using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TimeCloneAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TimeCloneAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static bool PostAct(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var directive = unit.GetDirectiveOrNull();
      if (directive is TimeScriptDirectiveUCAsIDirectiveUC tsdI) {
        var script = tsdI.obj.script;
        if (script.Count > 0) {
          script.RemoveAt(0);
        } else {
          // Unit should be deleted when they try to act again.
        }
      } else {
        Asserts.Assert(false);
      }
      return false;
    }

    public static IImpulse ProduceImpulse(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var directive = unit.GetDirectiveOrNull();
      if (directive is TimeScriptDirectiveUCAsIDirectiveUC tsdI) {
        var script = tsdI.obj.script;
        if (script.Count > 0) {
          var request = script[0];
          if (request is MoveRequestAsIRequest mrI) {
            var mr = mrI.obj;
            var destination = mr.destination;
            if (Actions.CanStep(game, superstate, unit, destination)) {
              return game.root.EffectMoveImpulseCreate(1000, destination).AsIImpulse();
            } else {
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
          } else if (request is AttackRequestAsIRequest arI) {
            // todo: implement attacking
            Asserts.Assert(false);
            return null;
          } else if (request is DefendRequestAsIRequest drI) {
            return game.root.EffectDefendImpulseCreate(1000).AsIImpulse();
          } else {
            Asserts.Assert(false, request.DStr());
            return null;
          }
        } else {
          return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
        }
      } else {
        Asserts.Assert(false);
        return null;
      }
    }
  }
}
