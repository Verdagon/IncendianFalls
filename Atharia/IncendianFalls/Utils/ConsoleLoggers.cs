using System;
using Atharia.Model;

namespace IncendianFalls {
  public class ConsoleLoggers {
    public class UnitsLogger : IUnitMutSetEffectObserver, IUnitMutSetEffectVisitor {
      public void OnUnitMutSetEffect(IUnitMutSetEffect effect) { effect.visit(this); }
      public void visitUnitMutSetAddEffect(UnitMutSetAddEffect effect) {
        Console.WriteLine("Unit added to bunch!");
      }
      public void visitUnitMutSetCreateEffect(UnitMutSetCreateEffect effect) {
        Console.WriteLine("Unit bunch created");
      }
      public void visitUnitMutSetDeleteEffect(UnitMutSetDeleteEffect effect) {
        Console.WriteLine("Unit bunch deleted");
      }
      public void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect) {
        Console.WriteLine("Unit removed from bunch!");
      }
    }

    public class UnitDetailsLogger : IIUnitComponentMutBunchEffectVisitor, IIUnitComponentMutBunchEffectObserver {
      public void OnIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffect effect) {
        effect.visit(this);
      }

      //public void visitIUCMutBunchAddEffect(IUCMutBunchAddEffect effect) {
      //  Console.WriteLine("Detail added to bunch!");
      //}

      public void visitIUnitComponentMutBunchCreateEffect(IUnitComponentMutBunchCreateEffect effect) {
        Console.WriteLine("Detail list made!");
      }

      public void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect effect) {
        Console.WriteLine("Detail list deleted!");
      }

      //public void visitIUCMutBunchRemoveEffect(IUCMutBunchRemoveEffect effect) {
      //  Console.WriteLine("Detail removed from bunch!");
      //}
    }

    public class PlayerLogger : IUnitEffectObserver, IUnitEffectVisitor {
      public void OnUnitEffect(IUnitEffect effect) {
        effect.visit(this);
      }
      public void visitUnitCreateEffect(UnitCreateEffect effect) {
        Console.WriteLine("Player created");
      }
      public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
        Console.WriteLine("Player deleting");
      }

      public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) {
        if (effect.newValue == false) {
          Console.WriteLine("Player has died!");
        }
      }

      public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
        Console.WriteLine("HP changed to " + effect.newValue);
      }
      //public void visitUnitSetMpEffect(UnitSetMpEffect effect) {
      //  Console.WriteLine("MP changed to " + effect.newValue);
      //}

      public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }

      public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
        Console.WriteLine("Location changed to " + effect.newValue);
      }
      public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) {
      }
    }

    public class PlayerEventLogger : IIUnitEventMutListEffectObserver, IIUnitEventMutListEffectVisitor {
      int playerId;
      public PlayerEventLogger(int playerId) {
        this.playerId = playerId;
      }

      public void OnIUnitEventMutListEffect(IIUnitEventMutListEffect effect) {
        effect.visit(this);
      }

      public void visitIUnitEventMutListAddEffect(IUnitEventMutListAddEffect addEffect) {
        if (addEffect.element is UnitStepEventAsIUnitEvent) {
          Console.WriteLine("Unit stepped!");
        } else if (addEffect.element is UnitAttackEventAsIUnitEvent) {
          var ev = ((UnitAttackEventAsIUnitEvent)addEffect.element).obj;
          if (ev.attackerId == playerId) {
            Console.WriteLine("Player attacked someone!");
          } else {
            Console.WriteLine("Player was attacked!");
          }
        }
      }
      public void visitIUnitEventMutListCreateEffect(IUnitEventMutListCreateEffect effect) { }
      public void visitIUnitEventMutListDeleteEffect(IUnitEventMutListDeleteEffect effect) { }
      public void visitIUnitEventMutListRemoveEffect(IUnitEventMutListRemoveEffect effect) { }
    }

    public class ConsoleLogger : ILogger {
      public void Error(string str) {
        Console.WriteLine(str);
      }
      public void Info(string str) {
        Console.WriteLine(str);
      }
      public void Warning(string str) {
        Console.WriteLine(str);
      }
    }

  }
}
