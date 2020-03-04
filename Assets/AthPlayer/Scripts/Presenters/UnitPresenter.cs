using System;
using AthPlayer;
using Atharia;
using Atharia.Model;
using UnityEngine;
using System.Collections.Generic;
using Domino;

namespace AthPlayer {
  public class UnitPresenter :
      IUnitEffectVisitor,
      IUnitEffectObserver,
      IIUnitEventMutListEffectVisitor,
      IIUnitEventMutListEffectObserver,
      IIUnitComponentMutBunchObserver,
      IBideAICapabilityUCEffectObserver,
      IBideAICapabilityUCEffectVisitor {
    bool alive;
    IClock clock;
    ITimer timer;
    SoundPlayer soundPlayer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;
    Game game;
    public readonly Unit unit;
    Instantiator instantiator;
    NarrationPanelView narrator;
    bool isRavashrike;

    UnitView unitView;
    HashSet<int> shieldingIds = new HashSet<int>();
    HashSet<int> counteringIds = new HashSet<int>();
    HashSet<int> bidingIds = new HashSet<int>();

    IUnitComponentMutBunchBroadcaster componentsBroadcaster;

    private UnitDescription description;

    public UnitPresenter(
      IClock clock,
        ITimer timer,
        SoundPlayer soundPlayer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
        Game game,
        Atharia.Model.Terrain terrain,
        Unit unit,
        Instantiator instantiator,
        NarrationPanelView narrator) {
      this.alive = true;
      this.timer = timer;
      this.soundPlayer = soundPlayer;
      this.resumeStaller = resumeStaller;
      this.turnStaller = turnStaller;
      this.game = game;
      this.unit = unit;
      this.instantiator = instantiator;
      this.narrator = narrator;
      this.isRavashrike = (unit.classId == "Ravashrike");

      unit.AddObserver(this);
      unit.events.AddObserver(this);

      this.description = GetUnitViewDescription(unit);

      unitView =
          instantiator.CreateUnitView(
            clock,
              timer,
              terrain.GetTileCenter(unit.location).ToUnity(),
              GetUnitViewDescription(unit),
              game.level.cameraAngle.ToUnity());

      componentsBroadcaster = new IUnitComponentMutBunchBroadcaster(unit.components);
      componentsBroadcaster.AddObserver(this);

      if (unit.components.GetOnlyBideAICapabilityUCOrNull().Exists()) {
        unit.components.GetOnlyBideAICapabilityUCOrNull().AddObserver(this);
      }
    }

    public void DestroyUnitPresenter() {
      if (unit.Exists()) {
        if (unit.components.GetOnlyBideAICapabilityUCOrNull().Exists()) {
          unit.components.GetOnlyBideAICapabilityUCOrNull().RemoveObserver(this);
        }

        componentsBroadcaster.RemoveObserver(this);
        componentsBroadcaster.Stop();

        unit.events.RemoveObserver(this);
        unit.RemoveObserver(this);
      }

      DestroyView();
      alive = false;
    }

    public void OnUnitEffect(IUnitEffect effect) {
      //if (unit.Exists()) {
      //  description = GetUnitViewDescription(unit);
      //}
        effect.visit(this);
    }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      if (isRavashrike) {
        narrator.ShowMessage("You have slain the Ravashrike and found Volcaetus!\nCongratulations on your glorious victory!");
      }

      // Note the lack of a DestroyUnitPresenter() here, that's done by GamePresenter when
      // it's removed from the level's units set.
    }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      var newPosition = game.level.terrain.GetTileCenter(unit.location).ToUnity();
      unitView.GetComponent<UnitView>().HopTo(newPosition);
      // If it's the player or a time shift clone, stall the next turn until we're done.
      if (unit.good) {
            turnStaller.StallForDuration(UnitView.HOP_DURATION_MS);
      }
    }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
      unitView.SetDescription(GetUnitViewDescription(unit));
    }
    public void visitUnitSetMpEffect(UnitSetMpEffect effect) {
      unitView.SetDescription(GetUnitViewDescription(unit));
    }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) {
      if (!effect.newValue) {
        if (isRavashrike) {
          narrator.ShowMessage("You have slain the Ravashrike and found Volcaetus!\nCongratulations on your glorious victory!");
        }

        unitView.GetComponent<UnitView>().Die(500);
        timer.ScheduleTimer(500, () => {
          if (unitView != null) {
            unitView.SetUnitViewActive(false);
          }
        });
      }
    }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }

    private void DestroyView() {
      unitView.DestroyUnit();
      unitView = null;
    }


    public void OnBideAICapabilityUCEffect(IBideAICapabilityUCEffect effect) { effect.visit(this); }
    public void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect effect) { }
    public void visitBideAICapabilityUCDeleteEffect(BideAICapabilityUCDeleteEffect effect) { }
    public void visitBideAICapabilityUCSetChargeEffect(BideAICapabilityUCSetChargeEffect effect) {
      Asserts.Assert(unit.Exists());
      unitView.SetDescription(GetUnitViewDescription(unit));
    }

    public void OnIUnitEventMutListEffect(IIUnitEventMutListEffect effect) {
        effect.visit(this);
    }
    public void visitIUnitEventMutListCreateEffect(IUnitEventMutListCreateEffect effect) { }
    public void visitIUnitEventMutListDeleteEffect(IUnitEventMutListDeleteEffect effect) { }
    public void visitIUnitEventMutListAddEffect(IUnitEventMutListAddEffect effect) {
      if (effect.element is UnitAttackEventAsIUnitEvent) {
        var evt = ((UnitAttackEventAsIUnitEvent)effect.element).obj;
        if (evt.attackerId == unit.id) {
          var victim = unit.root.GetUnit(evt.victimId);
          var victimPosition = game.level.terrain.GetTileCenter(victim.location).ToUnity();
          var playerPosition = game.level.terrain.GetTileCenter(unit.location).ToUnity();

          soundPlayer.Play("attack");

          unitView.Lunge((victimPosition - playerPosition).normalized * .25f);

          resumeStaller.StallForDuration(150);
          turnStaller.StallForDuration(350);
        }
      } else if (effect.element is UnitUnleashBideEventAsIUnitEvent) {
        unitView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "r",
                            50,
                    new UnityEngine.Color(1.0f, 1f, 1f, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)),
                true,
                new UnityEngine.Color(0, 0, 1f, 1f)));
      } else if (effect.element is UnitShieldingEventAsIUnitEvent) {
        unitView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "q",
                            50,
                    new UnityEngine.Color(1.0f, 1f, 1f, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)),
                true,
                new UnityEngine.Color(0, 0, 1f, 1f)));
        timer.ScheduleTimer(1, delegate () {
          if (alive) {
            resumeStaller.StallForDuration(500);
            turnStaller.StallForDuration(500);
          }
        });
      } else if (effect.element is UnitCounteringEventAsIUnitEvent) {
        unitView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "v",
                            50,
                    new UnityEngine.Color(1.0f, 1f, 1f, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)),
                true,
                new UnityEngine.Color(0, 0, 1f, 1f)));
        resumeStaller.StallForDuration(500);
        turnStaller.StallForDuration(500);
      } else if (effect.element is UnitFireEventAsIUnitEvent ufe) {
        if (unit.id == ufe.obj.attackerId) {
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "w",
                            50,
                      new UnityEngine.Color(1.0f, 1f, 1f, 1.5f),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  true,
                  new UnityEngine.Color(0, 0, 1f, 1f)));
        } else if (unit.id == ufe.obj.victimId) {
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "r",
                            50,
                      new UnityEngine.Color(1.0f, .6f, 0, 1.5f),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(0.8f, .4f, 0, 1.5f)),
                  true,
                  new UnityEngine.Color(0, 0, 1f, 1f)));
        }
      } else {

      }
    }
    public void visitIUnitEventMutListRemoveEffect(IUnitEventMutListRemoveEffect effect) { }

    public void OnIUnitComponentMutBunchAdd(int id) {
      var component = game.root.GetIUnitComponentOrNull(id);
      if (!component.Exists()) {
      } else if (component is ShieldingUCAsIUnitComponent shielding) {
        shieldingIds.Add(id);
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is CounteringUCAsIUnitComponent countering) {
        counteringIds.Add(id);
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is WanderAICapabilityUCAsIUnitComponent) {
      } else if (component is AttackAICapabilityUCAsIUnitComponent) {
      } else if (component is BideAICapabilityUCAsIUnitComponent) {
      } else if (component is TimeCloneAICapabilityUCAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is ManaPotionAsIUnitComponent) {
      } else if (component is HealthPotionAsIUnitComponent) {
      } else if (component is ArmorAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is GlaiveAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is InertiaRingAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else {
        Asserts.Assert(false, "Unknown component: " + component);
      }
    }

    public void OnIUnitComponentMutBunchRemove(int id) {
      if (shieldingIds.Contains(id)) {
        shieldingIds.Remove(id);
        description = GetUnitViewDescription(unit);
        unitView.SetDescription(description);
      } else if (counteringIds.Contains(id)) {
        counteringIds.Remove(id);
        description = GetUnitViewDescription(unit);
        unitView.SetDescription(description);
      } else if (bidingIds.Contains(id)) {
        unitView.SetDescription(GetUnitViewDescription(unit));
        bidingIds.Remove(id);
      }
    }

    public static UnitDescription GetUnitViewDescription(Unit unit) {
      var detailSymbols = new List<KeyValuePair<int, ExtrudedSymbolDescription>>();
      foreach (var detail in unit.components) {
        if (detail is ShieldingUCAsIUnitComponent shielding) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  shielding.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "q",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                          new UnityEngine.Color(0, 0, 0)),
                      true,
                      new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is CounteringUCAsIUnitComponent countering) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  countering.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "v",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                          new UnityEngine.Color(0, 0, 0)),
                      true,
                      new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          var bide = bideI.obj;
          if (bide.charge > 0) {
            var color = new UnityEngine.Color(1, 1, 1, 1.5f);
            switch (bide.charge) {
              case 1:
                color = new UnityEngine.Color(160/255f, 1f, 0f, 1.5f);
                break;
              case 2:
                color = new UnityEngine.Color(1f, 64/255f, 0, 1.5f);
                break;
              case 3:
                color = new UnityEngine.Color(1, 0f, 0, 1.5f);
                break;
            }
            detailSymbols.Add(
                new KeyValuePair<int, ExtrudedSymbolDescription>(
                    bideI.id,
                    new ExtrudedSymbolDescription(
                        RenderPriority.SYMBOL,
                        new SymbolDescription(
                            "n",
                            50,
                            color,
                            0,
                            OutlineMode.WithBackOutline,
                            new UnityEngine.Color(0, 0, 0)),
                        false,
                        new UnityEngine.Color(1, 1, 1, 1.5f))));
          }
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent tc) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  tc.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "l",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                          new UnityEngine.Color(0, 0, 0)),
                      false,
                      new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is GlaiveAsIUnitComponent) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  detail.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "s",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                    new UnityEngine.Color(0, 0, 0)),
                      true, new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is InertiaRingAsIUnitComponent) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  detail.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "4",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                    new UnityEngine.Color(0, 0, 0)),
                      true, new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is ArmorAsIUnitComponent) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  detail.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "zero",
                            50,
                          new UnityEngine.Color(1, 1, 1, 1.5f),
                          0,
                          OutlineMode.WithBackOutline,
                          new UnityEngine.Color(0, 0, 0)),
                      true,
                      new UnityEngine.Color(1, 1, 1, 1.5f))));
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else {
          Debug.LogError("Unknown detail type: " + detail);
        }
      }

      float hpRatio = Mathf.Clamp01((float)unit.hp / unit.maxHp);
      float mpRatio = Mathf.Clamp01((float)unit.mp / unit.maxMp);

      var detailsByClassId = new Dictionary<string, UnitDescription>();
      detailsByClassId.Add(
          "Chronomancer",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, new UnityEngine.Color(0.7f, 0, 0)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "b",
                            50, new UnityEngine.Color(1, 1, 1, 1.5f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  true, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "avelisk",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, new UnityEngine.Color(1.0f, 0.6f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "x",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "novafaire",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, new UnityEngine.Color(0.25f, 0.25f, 1.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "y",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "draxling",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, new UnityEngine.Color(0.7f, 0.0f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "parenright",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "lornix",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, new UnityEngine.Color(0.0f, 0.6f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "ampersand",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "yoten",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, new UnityEngine.Color(0.33f, 0.33f, 0.33f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "z",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "spiriad",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, new UnityEngine.Color(1.0f, 1.0f, 1.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "parenleft",
                            50, new UnityEngine.Color(0, 0, 0, 1f), 0, OutlineMode.WithBackOutline,
                      new UnityEngine.Color(1, 1, 1)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "mordranth",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, new UnityEngine.Color(0.0f, 0f, 0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "three",
                            50, new UnityEngine.Color(1, 1, 1, 1f), 0, OutlineMode.NoOutline,
                      new UnityEngine.Color(1, 1, 1)),
                  false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Ravashrike",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, new UnityEngine.Color(0.7f, 0, 0.7f)),
              new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,

                new SymbolDescription(
              "m",
                            50, new UnityEngine.Color(1, 1, 1, 1.5f), 0, OutlineMode.WithBackOutline,
                    new UnityEngine.Color(0, 0, 0)), false, new UnityEngine.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      if (detailsByClassId.ContainsKey(unit.classId)) {
        return detailsByClassId[unit.classId];
      } else {
        Debug.LogError("Unknown class id: " + unit.classId);
        return new UnitDescription(
              unit.id,
            new DominoDescription(true, new UnityEngine.Color(1f, 1f, 0)),
            new ExtrudedSymbolDescription(
            RenderPriority.SYMBOL,

                new SymbolDescription(
            "a",
                            50, new UnityEngine.Color(0, 1f, 1f), 15, OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)), true, new UnityEngine.Color(0, 0, 0)),
            new List<KeyValuePair<int, ExtrudedSymbolDescription>>(),
            hpRatio,
              mpRatio);
      }
    }
  }
}
