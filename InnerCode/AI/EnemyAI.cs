using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class EnemyAI {
    private static void Idle(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {

      List<Location> pathToPlayer;
      if (Sight.CanSee(game, unit, game.player.location, out pathToPlayer)) {
        var directive =
            game.root.EffectAttackDirectiveCreate(
                game.player,
                game.root.EffectLocationMutListCreate(pathToPlayer));
        unit.directive = new AttackDirectiveAsIDirective(directive);
        AttackTo(game, liveUnitByLocationMap, unit, directive);
        return;
      }

      // Nothing else to do, just idle.
      unit.nextActionTime = unit.nextActionTime + unit.inertia / 2;
    }

    private static void AttackTo(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit,
        AttackDirective attack) {
      if (!attack.targetUnit.Exists() || !attack.targetUnit.alive) {
        attack.pathToLastSeenLocation.Delete();
        attack.Delete();
        Idle(game, liveUnitByLocationMap, unit);
        return;
      }

      if (attack.pathToLastSeenLocation.Count == 0) {
        Asserts.Assert(false, "Empty path to last seen?");
      }

      var lastSeenLocation =
          attack.pathToLastSeenLocation[attack.pathToLastSeenLocation.Count - 1];

      if (attack.targetUnit.location != lastSeenLocation) {
        List<Location> pathToVictim;
        if (Sight.CanSee(game, unit, attack.targetUnit.location, out pathToVictim)) {
          attack.pathToLastSeenLocation.Clear();
          attack.pathToLastSeenLocation.AddRange(pathToVictim);
          lastSeenLocation = attack.targetUnit.location;
        } else {
          // We can't see the player anymore. Follow the last known path.
        }
      } else {
        // The player hasn't moved, follow the last known path.
      }

      if (game.level.terrain.pattern.LocationsAreAdjacent(unit.location, attack.targetUnit.location, game.level.considerCornersAdjacent)) {
        Actions.Attack(game, liveUnitByLocationMap, unit, attack.targetUnit);
        return;
      }

      if (!Actions.CanStep(game, liveUnitByLocationMap, unit, attack.pathToLastSeenLocation[0])) {
        // Am confused. Can't step that way. This might be because another unit
        // walked in front of us or something. Keep the same directive, but stall
        // by half a turn.
        unit.nextActionTime = unit.nextActionTime + unit.inertia / 2;
        return;
      }

      Actions.Step(game, liveUnitByLocationMap, unit, attack.pathToLastSeenLocation[0]);
      attack.pathToLastSeenLocation.RemoveAt(0);

      if (attack.pathToLastSeenLocation.Count == 0) {
        // We made it and we can't find the player.
        attack.pathToLastSeenLocation.Delete();
        attack.Delete();
      }
    }

    private static void GoTowards(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit,
        MoveDirective move) {
      Asserts.Assert(move.path.Count > 0, "Empty path?");

      if (!Actions.CanStep(game, liveUnitByLocationMap, unit, move.path[0])) {
        // Am confused. Can't step that way. This might be because another unit
        // walked in front of us or something. Keep the same directive, but stall
        // by half a turn.
        unit.nextActionTime = unit.nextActionTime + unit.inertia / 2;
        return;
      }

      Actions.Step(game, liveUnitByLocationMap, unit, move.path[0]);
      move.path.RemoveAt(0);

      if (move.path.Count == 0) {
        // We made it and we can't find the player.
        move.path.Delete();
        move.Delete();
      }
    }

    public static void AI(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {

      var unitPosition = game.level.terrain.pattern.GetTileCenter(unit.location);

      if (!unit.directive.Exists()) {
        Idle(game, liveUnitByLocationMap, unit);
      } else if (unit.directive is MoveDirectiveAsIDirective move) {
        GoTowards(game, liveUnitByLocationMap, unit, move.obj);
      } else if (unit.directive is AttackDirectiveAsIDirective attack) {
        AttackTo(game, liveUnitByLocationMap, unit, attack.obj);
      }
    }
  }
}
