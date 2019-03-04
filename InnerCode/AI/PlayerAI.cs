using System;
using Atharia.Model;

namespace IncendianFalls {
  public class PlayerAI {
    public static bool AI(Game game, Superstate superstate) {
      if (game.player.GetDirectiveOrNull() is MoveDirectiveUCAsIDirectiveUC mdI) {
        return FollowMoveDirective(game, superstate, game.player);
      } else if (game.player.GetDirectiveOrNull() is TimeScriptDirectiveUCAsIDirectiveUC tsdI) {
        var tsd = tsdI.obj;
        if (tsd.script.Count > 0) {
          var request = tsd.script[0];
          if (request is MoveRequestAsIRequest mrI) {
            var mr = mrI.obj;
            var destination = mr.destination;
            if (Actions.CanStep(game, superstate, game.player, destination)) {
              Actions.Step(game, superstate, game.player, destination);
              return true;
            } else {
              Asserts.Assert(false); // todo: implement evaporate
              return true;
            }
          } else if (request is AttackRequestAsIRequest arI) {
            // todo: implement attacking
            Asserts.Assert(false);
            return true;
          } else {
            Asserts.Assert(false, request.DStr());
            return false;
          }
        } else {
          Asserts.Assert(false); // todo: implement evaporate
          return true;
        }
      } else {
        Asserts.Assert(false);
        return false;
      }
    }

    private static bool FollowMoveDirective(
        Game game,
        Superstate superstate,
        Unit unit) {
      var idirective = unit.components.GetOnlyIDirectiveUCOrNull();
      Asserts.Assert(idirective is MoveDirectiveUCAsIDirectiveUC move);
      var directive = (idirective as MoveDirectiveUCAsIDirectiveUC).obj;

      if (!Actions.CanStep(game, superstate, game.player, directive.path[0])) {
        game.root.logger.Info("Blocked!");
        return false;
      }

      Actions.Step(game, superstate, game.player, directive.path[0]);
      directive.path.RemoveAt(0);

      if (directive.path.Count == 0) {
        unit.ClearDirective();
      }

      return true;
    }
  }
}
