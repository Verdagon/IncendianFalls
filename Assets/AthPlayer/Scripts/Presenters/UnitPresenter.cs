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
      IIUnitComponentMutBunchObserver,
      IBideAICapabilityUCEffectObserver,
      IBideAICapabilityUCEffectVisitor,
      ISorcerousUCEffectObserver,
      ISorcerousUCEffectVisitor {
    public delegate void IOnAnimation(long endGameTimeMs);
    public event IOnAnimation onAnimation;

    bool instanceAlive;
    IClock clock;
    ITimer timer;
    SoundPlayer soundPlayer;
    EffectBroadcaster preBroadcaster;
    EffectBroadcaster postBroadcaster;
    Game game;
    public readonly Unit unit;
    Instantiator instantiator;

    UnitView unitView;

    IUnitComponentMutBunchBroadcaster componentsBroadcaster;

    // We dont want two runes to appear at the same time.
    long runeEndTime;
    // We want hops to wait for each other.
    long hopEndTime;
    // We want lunges to wait for each other.
    long lungeEndTime;
    // We want delete to wait for death to finish.
    long dieEndTime;
    // This will pay attention to the above endTimes and report stalls to the effect staller.
    UnitEffectStaller effectStaller;

    UnitEffectPreReactor effectPreReactor;

    public UnitPresenter(
      IClock clock,
        ITimer timer,
        SoundPlayer soundPlayer,
        EffectBroadcaster stallBroadcaster,
        IEffectStaller stallEffect,
        EffectBroadcaster preBroadcaster,
        EffectBroadcaster postBroadcaster,
        Game game,
        Atharia.Model.Terrain terrain,
        Unit unit,
        Instantiator instantiator) {
      this.instanceAlive = true;
      this.preBroadcaster = preBroadcaster;
      this.postBroadcaster = postBroadcaster;
      this.timer = timer;
      this.soundPlayer = soundPlayer;
      this.game = game;
      this.clock = clock;
      this.unit = unit;
      this.instantiator = instantiator;

      unit.AddObserver(postBroadcaster, this);
      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        sorcerous.AddObserver(postBroadcaster, this);
      }

      effectPreReactor = new UnitEffectPreReactor();
      unit.AddObserver(preBroadcaster, effectPreReactor);

      componentsBroadcaster = new IUnitComponentMutBunchBroadcaster(postBroadcaster, unit.components);
      componentsBroadcaster.AddObserver(this);

      unitView =
          instantiator.CreateUnitView(
            clock,
              timer,
              terrain.GetTileCenter(unit.location).ToUnity(),
              GetUnitViewDescription(unit),
              game.level.cameraAngle.ToUnity());

      if (unit.components.GetOnlyBideAICapabilityUCOrNull().Exists()) {
        unit.components.GetOnlyBideAICapabilityUCOrNull().AddObserver(postBroadcaster, this);
      }

      this.effectStaller = new UnitEffectStaller(stallBroadcaster, game, this, stallEffect);
    }

    public (long, UnitView) DestroyUnitPresenter() {
      Asserts.Assert(instanceAlive);
      Asserts.Assert(unit.Exists());

      effectStaller.Destroy();

      if (unit.components.GetOnlyBideAICapabilityUCOrNull().Exists()) {
        unit.components.GetOnlyBideAICapabilityUCOrNull().RemoveObserver(postBroadcaster, this);
      }

      unit.RemoveObserver(preBroadcaster, effectPreReactor);

      componentsBroadcaster.RemoveObserver(this);
      componentsBroadcaster.Stop();

      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        sorcerous.RemoveObserver(postBroadcaster, this);
      }
      unit.RemoveObserver(postBroadcaster, this);

      long animationsEndTime =
        Math.Max(hopEndTime,
          Math.Max(lungeEndTime,
            Math.Max(runeEndTime,
              dieEndTime)));
      if (clock.GetTimeMs() >= animationsEndTime) {
        unitView.Destruct();
        unitView = null;
      }

      instanceAlive = false;
      return (animationsEndTime, unitView);
    }

    public void OnUnitEffect(IUnitEffect effect) {
      Asserts.Assert(instanceAlive); 
      effect.visitIUnitEffect(this);
    }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      // Note the lack of a DestroyUnitPresenter() here, that's done by GamePresenter when
      // it's removed from the level's units set.
    }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      var newPosition = game.level.terrain.GetTileCenter(unit.location).ToUnity();
      hopEndTime = unitView.GetComponent<UnitView>().HopTo(newPosition);
      onAnimation?.Invoke(hopEndTime);

      //// If it's the player or a time shift clone, stall the next turn until we're done.
      //if (unit.good) {
      //  this.hopEndTime = hopEndTime;
      //}
    }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
      unitView.SetDescription(GetUnitViewDescription(unit));
    }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) {
      if (effect.newValue != 0) {
        dieEndTime = unitView.Die();
        onAnimation?.Invoke(dieEndTime);
      }
    }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void OnSorcerousUCEffect(ISorcerousUCEffect effect) {
      Asserts.Assert(instanceAlive); 
      effect.visitISorcerousUCEffect(this);
    }
    public void visitSorcerousUCCreateEffect(SorcerousUCCreateEffect effect) { }
    public void visitSorcerousUCDeleteEffect(SorcerousUCDeleteEffect effect) { }
    public void visitSorcerousUCSetMpEffect(SorcerousUCSetMpEffect effect) {
      unitView.SetDescription(GetUnitViewDescription(unit));
    }
    public void visitSorcerousUCSetMaxMpEffect(SorcerousUCSetMaxMpEffect effect) {
      unitView.SetDescription(GetUnitViewDescription(unit));
    }

    public void OnBideAICapabilityUCEffect(IBideAICapabilityUCEffect effect) {
      Asserts.Assert(instanceAlive); 
      effect.visitIBideAICapabilityUCEffect(this);
    }
    public void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect effect) { }
    public void visitBideAICapabilityUCDeleteEffect(BideAICapabilityUCDeleteEffect effect) { }
    public void visitBideAICapabilityUCSetChargeEffect(BideAICapabilityUCSetChargeEffect effect) {
      Asserts.Assert(unit.Exists());
      unitView.SetDescription(GetUnitViewDescription(unit));
    }

    public void OnIUnitComponentMutBunchAdd(int id) {
      var component = game.root.GetIUnitComponentOrNull(id);
      Asserts.Assert(component.Exists());
      if (component is InvincibilityUCAsIUnitComponent invincibility) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is DefyingUCAsIUnitComponent defying) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is MiredUCAsIUnitComponent mired) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is CounteringUCAsIUnitComponent countering) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is WanderAICapabilityUCAsIUnitComponent) {
      } else if (component is KamikazeAICapabilityUCAsIUnitComponent) {
      } else if (component is AttackAICapabilityUCAsIUnitComponent) {
      } else if (component is BideAICapabilityUCAsIUnitComponent) {
      } else if (component is SorcerousUCAsIUnitComponent sorc) {
        sorc.obj.AddObserver(postBroadcaster, this);
      } else if (component is BaseCombatTimeUCAsIUnitComponent) {
      } else if (component is SummonAICapabilityUCAsIUnitComponent) {
      } else if (component is TemporaryCloneAICapabilityUC) {
      } else if (component is BaseMovementTimeUCAsIUnitComponent) {
      } else if (component is BaseSightRangeUCAsIUnitComponent) {
      } else if (component is LightningChargingUCAsIUnitComponent) {
      } else if (component is LightningChargedUCAsIUnitComponent) {
      } else if (component is TutorialDefyCounterUCAsIUnitComponent) {
      } else if (component is BaseOffenseUCAsIUnitComponent) {
      } else if (component is BaseDefenseUCAsIUnitComponent) {
      } else if (component is DoomedUCAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is TemporaryCloneAICapabilityUCAsIUnitComponent) {
      } else if (component is TimeCloneAICapabilityUCAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is ManaPotionAsIUnitComponent) {
      } else if (component is HealthPotionAsIUnitComponent) {
      } else if (component is SlowRodAsIUnitComponent) {
      } else if (component is BlastRodAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is ArmorAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is GlaiveAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is SpeedRingAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else {
        Asserts.Assert(false, "Unknown component: " + component);
      }
    }

    public void OnIUnitComponentMutBunchRemove(int id) {
      var component = game.root.GetIUnitComponentOrNull(id);
      Asserts.Assert(component.Exists());
      if (component is InvincibilityUCAsIUnitComponent invincibility) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is DefyingUCAsIUnitComponent defying) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is MiredUCAsIUnitComponent mired) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is CounteringUCAsIUnitComponent countering) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is WanderAICapabilityUCAsIUnitComponent) {
      } else if (component is KamikazeAICapabilityUCAsIUnitComponent) {
      } else if (component is AttackAICapabilityUCAsIUnitComponent) {
      } else if (component is BideAICapabilityUCAsIUnitComponent) {
      } else if (component is SorcerousUCAsIUnitComponent sorc) {
        sorc.obj.RemoveObserver(postBroadcaster, this);
      } else if (component is BaseCombatTimeUCAsIUnitComponent) {
      } else if (component is SummonAICapabilityUCAsIUnitComponent) {
      } else if (component is TemporaryCloneAICapabilityUC) {
      } else if (component is BaseMovementTimeUCAsIUnitComponent) {
      } else if (component is BaseSightRangeUCAsIUnitComponent) {
      } else if (component is LightningChargingUCAsIUnitComponent) {
      } else if (component is LightningChargedUCAsIUnitComponent) {
      } else if (component is TutorialDefyCounterUCAsIUnitComponent) {
      } else if (component is BaseOffenseUCAsIUnitComponent) {
      } else if (component is BaseDefenseUCAsIUnitComponent) {
      } else if (component is DoomedUCAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is TemporaryCloneAICapabilityUCAsIUnitComponent) {
      } else if (component is TimeCloneAICapabilityUCAsIUnitComponent) {
        unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is ManaPotionAsIUnitComponent) {
      } else if (component is HealthPotionAsIUnitComponent) {
      } else if (component is SlowRodAsIUnitComponent) {
      } else if (component is BlastRodAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is ArmorAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is GlaiveAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else if (component is SpeedRingAsIUnitComponent) {
        //unitView.SetDescription(GetUnitViewDescription(unit));
      } else {
        Asserts.Assert(false, "Unknown component: " + component);
      }
    }

    public static UnitDescription GetUnitViewDescription(Unit unit) {
      var detailSymbols = new List<KeyValuePair<int, ExtrudedSymbolDescription>>();
      foreach (var detail in unit.components) {
        if (detail is DefyingUCAsIUnitComponent defying) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  defying.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "q",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      true,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is InvincibilityUCAsIUnitComponent inv) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  inv.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "q",
                          Vector4Animation.Color(0, .5f, 1, 1.5f),
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      true,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is MiredUCAsIUnitComponent mired) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  mired.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "f-8",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      true,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is CounteringUCAsIUnitComponent countering) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  countering.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "v",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      true,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is TutorialDefyCounterUCAsIUnitComponent) {
        } else if (detail is LightningChargingUCAsIUnitComponent charging) {
        } else if (detail is LightningChargedUCAsIUnitComponent charged) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  charged.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r-3",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      true,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is KamikazeAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is GuardAICapabilityUCAsIUnitComponent) {
        } else if (detail is SorcerousUCAsIUnitComponent) {
        } else if (detail is BaseCombatTimeUCAsIUnitComponent) {
        } else if (detail is SummonAICapabilityUCAsIUnitComponent) {
        } else if (detail is BaseMovementTimeUCAsIUnitComponent) {
        } else if (detail is BaseSightRangeUCAsIUnitComponent) {
        } else if (detail is BaseOffenseUCAsIUnitComponent) {
        } else if (detail is BaseDefenseUCAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          var bide = bideI.obj;
          if (bide.charge > 0) {
            var color = Vector4Animation.GLOWY_WHITE;
            switch (bide.charge) {
              case 1:
                color = Vector4Animation.Color(160/255f, 1f, 0f, 1.5f);
                break;
              case 2:
                color = Vector4Animation.Color(1f, 64/255f, 0, 1.5f);
                break;
              case 3:
                color = Vector4Animation.Color(1, 0f, 0, 1.5f);
                break;
            }
            detailSymbols.Add(
                new KeyValuePair<int, ExtrudedSymbolDescription>(
                    bideI.id,
                    new ExtrudedSymbolDescription(
                        RenderPriority.SYMBOL,
                        new SymbolDescription(
                            "n",
                            color,
                            0,
                            1,
                            OutlineMode.WithBackOutline,
                            Vector4Animation.Color(0, 0, 0)),
                        false,
                        Vector4Animation.GLOWY_WHITE)));
          }
        } else if (detail is TemporaryCloneAICapabilityUCAsIUnitComponent tca) {
        } else if (detail is DoomedUCAsIUnitComponent d) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  d.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "l",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      false,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent tc) {
          detailSymbols.Add(
              new KeyValuePair<int, ExtrudedSymbolDescription>(
                  tc.id,
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "l",
                          Vector4Animation.GLOWY_WHITE,
                          0,
                          1,
                          OutlineMode.WithBackOutline),
                      false,
                      Vector4Animation.GLOWY_WHITE)));
        } else if (detail is GlaiveAsIUnitComponent) {
          //detailSymbols.Add(
          //    new KeyValuePair<int, ExtrudedSymbolDescription>(
          //        detail.id,
          //        new ExtrudedSymbolDescription(
          //            RenderPriority.SYMBOL,
          //            new SymbolDescription(
          //                "s",
          //                  50,
          //                Vector4Animation.GLOWY_WHITE,
          //                0,
          //                OutlineMode.WithBackOutline,
          //          Vector4Animation.Color(0, 0, 0)),
          //            true, Vector4Animation.GLOWY_WHITE)));
        } else if (detail is SpeedRingAsIUnitComponent) {
          //detailSymbols.Add(
          //    new KeyValuePair<int, ExtrudedSymbolDescription>(
          //        detail.id,
          //        new ExtrudedSymbolDescription(
          //            RenderPriority.SYMBOL,
          //            new SymbolDescription(
          //                "4",
          //                  50,
          //                Vector4Animation.GLOWY_WHITE,
          //                0,
          //                OutlineMode.WithBackOutline,
          //          Vector4Animation.Color(0, 0, 0)),
          //            true, Vector4Animation.GLOWY_WHITE)));
        } else if (detail is ArmorAsIUnitComponent) {
          //detailSymbols.Add(
          //    new KeyValuePair<int, ExtrudedSymbolDescription>(
          //        detail.id,
          //        new ExtrudedSymbolDescription(
          //            RenderPriority.SYMBOL,
          //            new SymbolDescription(
          //                "zero",
          //                  50,
          //                Vector4Animation.GLOWY_WHITE,
          //                0,
          //                OutlineMode.WithBackOutline,
          //                Vector4Animation.Color(0, 0, 0)),
          //            true,
          //            Vector4Animation.GLOWY_WHITE)));
        } else if (detail is SlowRodAsIUnitComponent) {
        } else if (detail is BlastRodAsIUnitComponent) {
          //detailSymbols.Add(
          //    new KeyValuePair<int, ExtrudedSymbolDescription>(
          //        detail.id,
          //        new ExtrudedSymbolDescription(
          //            RenderPriority.SYMBOL,
          //            new SymbolDescription(
          //                "w",
          //                  50,
          //                Vector4Animation.GLOWY_WHITE,
          //                0,
          //                OutlineMode.WithBackOutline,
          //                Vector4Animation.Color(0, 0, 0)),
          //            true,
          //            Vector4Animation.GLOWY_WHITE)));
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else {
          Debug.LogError("Unknown detail type: " + detail);
        }
      }

      float hpRatio = Mathf.Clamp01((float)unit.hp / unit.maxHp);
      float mpRatio = 1;
      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        mpRatio = Mathf.Clamp01((float)sorcerous.mp / sorcerous.maxMp);
      }

      var detailsByClassId = new Dictionary<string, UnitDescription>();
      detailsByClassId.Add(
          "Chronomancer",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.0f, 0.2f, 1f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "b",
                      new Vector4Animation(
                        new ConstantFloatAnimation(1),
                        SineFloatAnimation.Make(.5f), // the half is to slow it down a little
                        new ConstantFloatAnimation(1),
                        new ConstantFloatAnimation(1.2f)),
                      0,
                      1,
                      OutlineMode.WithBackOutline),
                  true, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "avelisk",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(1.0f, 0.6f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "x",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "novafaire",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.25f, 0.25f, 1.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "y",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "draxling",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.2f, 0.4f, 0.5f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "percent",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "lornix",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.0f, 0.6f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "ampersand",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "yoten",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.33f, 0.33f, 0.33f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "z",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "spiriad",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(1.0f, 1.0f, 1.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "parenleft",
                            Vector4Animation.Color(0, 0, 0, 1f), 0, 1, OutlineMode.WithBackOutline,
                      Vector4Animation.Color(1, 1, 1)),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "mordranth",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.0f, 0f, 0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "three",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.NoOutline,
                      Vector4Animation.Color(1, 1, 1)),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Emberfolk",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.7f, 0.35f, 0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "r-8",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.NoOutline,
                      Vector4Animation.Color(1, 1, 1)),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Irkling",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.5f, 0.2f, 0)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "ampersand",
                            Vector4Animation.Color(1f,1f,1f, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Baug",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.3f, 0.3f, 0.3f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "f-8",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Spirient",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.0f, 0.0f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "parenleft",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "RavagianTrask",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0f, 0.5f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "x",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "MantisBombardier",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.0f, 0.4f, 0.0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "three",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Chronolisk",
          new UnitDescription(
              unit.id,
              new DominoDescription(false, Vector4Animation.Color(0.5f, 0.5f, 0.5f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "k",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "MysteriousMan",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.5f, 0.5f, 0.5f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "y",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "LightningTrask",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.7f, 0f, 0f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "x",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "IrklingKing",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(.7f, 0, .6f, 1f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "ampersand",
                            Vector4Animation.Color(1, 1, 1, 1f), 0, 1, OutlineMode.WithBackOutline),
                  false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      detailsByClassId.Add(
          "Ravashrike",
          new UnitDescription(
              unit.id,
              new DominoDescription(true, Vector4Animation.Color(0.7f, 0, 0.7f)),
              new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,

                new SymbolDescription(
              "m",
                            Vector4Animation.GLOWY_WHITE, 0, 1, OutlineMode.WithBackOutline), false, Vector4Animation.Color(0, 0, 0)),
              detailSymbols,
              hpRatio,
              mpRatio));
      if (detailsByClassId.ContainsKey(unit.classId)) {
        return detailsByClassId[unit.classId];
      } else {
        Debug.LogError("Unknown class id: " + unit.classId);
        return new UnitDescription(
              unit.id,
            new DominoDescription(true, Vector4Animation.Color(1f, 1f, 0)),
            new ExtrudedSymbolDescription(
            RenderPriority.SYMBOL,

                new SymbolDescription(
            "a",
                            Vector4Animation.Color(0, 1f, 1f), 15, 1, OutlineMode.WithOutline,
                    Vector4Animation.Color(0, 0, 0)), true, Vector4Animation.Color(0, 0, 0)),
            new List<KeyValuePair<int, ExtrudedSymbolDescription>>(),
            hpRatio,
              mpRatio);
      }
    }

    public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) {
      Asserts.Assert(instanceAlive);
      if (effect.newValue is UnitAttackEventAsIUnitEvent) {
        var evt = ((UnitAttackEventAsIUnitEvent)effect.newValue).obj;
        Asserts.Assert(unit.Exists());
        if (evt.attackerId == unit.id) {
          var victim = unit.root.GetUnit(evt.victimId);
          Asserts.Assert(victim.Exists());
          var victimPosition = game.level.terrain.GetTileCenter(victim.location).ToUnity();
          var attackerPosition = game.level.terrain.GetTileCenter(unit.location).ToUnity();

          soundPlayer.Play("attack");

          lungeEndTime =
            unitView.Lunge((victimPosition - attackerPosition).normalized * .25f);
          onAnimation?.Invoke(lungeEndTime);
        }
      } else if (effect.newValue is UnitUnleashBideEventAsIUnitEvent) {
        runeEndTime =
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "r-3",
                      Vector4Animation.Color(1.0f, 1f, 1f, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
        onAnimation?.Invoke(runeEndTime);
      } else if (effect.newValue is UnitDefyingEventAsIUnitEvent) {
        runeEndTime =
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "q",
                      Vector4Animation.Color(1.0f, 1f, 1f, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
        onAnimation?.Invoke(runeEndTime);
      } else if (effect.newValue is UnitCounteringEventAsIUnitEvent) {
        runeEndTime =
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "v",
                      Vector4Animation.Color(1.0f, 1f, 1f, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
        onAnimation?.Invoke(runeEndTime);
      } else if (effect.newValue is UnitFireEventAsIUnitEvent ufe) {
        if (unit.id == ufe.obj.attackerId) {
          runeEndTime =
            unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "w",
                      Vector4Animation.Color(1.0f, 1f, 1f, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
          onAnimation?.Invoke(runeEndTime);
        } else if (unit.id == ufe.obj.victimId) {
          runeEndTime =
            unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "r-3",
                      Vector4Animation.Color(1.0f, .6f, 0, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0.8f, .4f, 0, 1.5f)),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
          onAnimation?.Invoke(runeEndTime);
        }
      } else if (effect.newValue is UnitFireBombedEventAsIUnitEvent ufbe) {
        runeEndTime =
          unitView.ShowRune(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "r-3",
                      Vector4Animation.Color(1.0f, .6f, 0, 1.5f),
                      0,
                      1,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0.8f, .4f, 0, 1.5f)),
                  true,
                  Vector4Animation.Color(0, 0, 1f, 1f)));
        onAnimation?.Invoke(runeEndTime);
      } else {

      }
    }

    private class UnitEffectPreReactor : IUnitEffectObserver, IUnitEffectVisitor {
      private UnitPresenter unitPresenter;

      public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
      public void visitUnitCreateEffect(UnitCreateEffect effect) { }
      public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) { }
      public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
      public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
      public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
      public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
      public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
      public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
        //unitPresenter.BeforeDelete();
      }
    }

    // This class gets to preview an effect before it officially happens.
    // Its main purpose is to stall the effect until this UnitPresenter is ready.
    private class UnitEffectStaller :
        IUnitEffectObserver,
        IUnitEffectVisitor,
        IIUnitComponentMutBunchObserver,
        IIUnitComponentMutBunchEffectVisitor,
        IGameEffectObserver,
        IGameEffectVisitor {

      EffectBroadcaster stallBroadcaster;
      Game game;
      IUnitComponentMutBunchBroadcaster componentsBroadcaster;
      private UnitPresenter unitPresenter;
      private IEffectStaller staller;

      public UnitEffectStaller(
          EffectBroadcaster stallBroadcaster,
          Game game,
          UnitPresenter unitPresenter,
          IEffectStaller staller) {
        this.stallBroadcaster = stallBroadcaster;
        this.game = game;
        this.unitPresenter = unitPresenter;
        this.staller = staller;

        game.AddObserver(stallBroadcaster, this);

        stallBroadcaster.AddUnitObserver(unitPresenter.unit.id, this);

        this.componentsBroadcaster = new IUnitComponentMutBunchBroadcaster(stallBroadcaster, unitPresenter.unit.components);
        componentsBroadcaster.AddObserver(this);
      }

      public void Destroy() {
        componentsBroadcaster.RemoveObserver(this);
        stallBroadcaster.RemoveUnitObserver(unitPresenter.unit.id, this);
        game.AddObserver(stallBroadcaster, this);
      }

      private void StallForAllAnimations(bool includingDeath) {
        staller(unitPresenter.hopEndTime, "unit hop");
        staller(unitPresenter.lungeEndTime, "unit lunge");
        staller(unitPresenter.runeEndTime, "unit rune");
        if (includingDeath) {
          staller(unitPresenter.dieEndTime, "unit die");
        }
      }

      public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
      public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
      public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
      public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
      public void visitUnitCreateEffect(UnitCreateEffect effect) { }

      public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
        StallForAllAnimations(true);
      }

      public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) {
        if (effect.newValue is WaitForUnitEventAsIUnitEvent) {
          StallForAllAnimations(true);
        } else if (effect.newValue is UnitAttackEventAsIUnitEvent a) {
          if (a.obj.attackerId == unitPresenter.unit.id) {
            staller(unitPresenter.hopEndTime, "unit hop");
            staller(unitPresenter.lungeEndTime, "unit lunge");
          }
        } else if (effect.newValue is UnitCounteringEventAsIUnitEvent) {
          staller(unitPresenter.runeEndTime, "unit rune");
        } else if (effect.newValue is UnitDefyingEventAsIUnitEvent) {
          staller(unitPresenter.runeEndTime, "unit rune");
        } else if (effect.newValue is UnitUnleashBideEventAsIUnitEvent) {
          staller(unitPresenter.runeEndTime, "unit rune");
        } else if (effect.newValue is UnitFireEventAsIUnitEvent f) {
          // We show a rune for both attacker and victim, so no need to check that.
          staller(unitPresenter.runeEndTime, "unit rune");
        } else if (effect.newValue is UnitFireBombedEventAsIUnitEvent) {
          staller(unitPresenter.runeEndTime, "unit rune");
        }
      }

      public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) {
        StallForAllAnimations(false);
      }

      public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
        staller(unitPresenter.hopEndTime, "unit hop");
      }

      public void OnIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffect effect) { effect.visitIIUnitComponentMutBunchEffect(this); }
      public void visitIUnitComponentMutBunchCreateEffect(IUnitComponentMutBunchCreateEffect effect) { }
      public void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect effect) { }
      public void OnIUnitComponentMutBunchRemove(int id) { }
      public void OnIUnitComponentMutBunchAdd(int id) {
        //var unitComponent = unitPresenter.unit.root.GetIUnitComponent(id);
        //if (unitComponent is DefyingUCAsIUnitComponent) {
        //} else {
        //}
      }

      public void OnGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
      public void visitGameCreateEffect(GameCreateEffect effect) { }
      public void visitGameDeleteEffect(GameDeleteEffect effect) { }
      public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) { }
      public void visitGameSetLevelEffect(GameSetLevelEffect effect) { }
      public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
      public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) { }
      public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) { }
      public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) { }
      public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) { }
      public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) { }
      public void visitGameSetEvventEffect(GameSetEvventEffect effect) {
        if (effect.newValue is WaitForEverythingEventAsIGameEvent) {
          StallForAllAnimations(true);
        }
      }
    }
  }
}
