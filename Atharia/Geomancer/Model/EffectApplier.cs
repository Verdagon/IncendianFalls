using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class EffectApplier : IEffectVisitor ,
ITerrainTileEffectVisitor,
ITerrainEffectVisitor,
ILevelEffectVisitor,
IRandEffectVisitor,
IStrMutListEffectVisitor,
ITerrainTileByLocationMutMapEffectVisitor {
  Root root;
  public EffectApplier(Root root) {
    this.root = root;
  }


public void visitTerrainTileEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) {
    var instance = root.EffectTerrainTileCreate(
  effect.incarnation.elevation,
  root.GetStrMutList(effect.incarnation.members)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect) {
    root.EffectTerrainTileDelete(effect.id);
  }

     
  public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect) {
    root.EffectTerrainTileSetElevation(
      effect.id,
  effect.newValue
    );
  }

public void visitTerrainEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
  public void visitTerrainCreateEffect(TerrainCreateEffect effect) {
    var instance = root.EffectTerrainCreate(
  effect.incarnation.pattern,
  effect.incarnation.elevationStepHeight,
  root.GetTerrainTileByLocationMutMap(effect.incarnation.tiles)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) {
    root.EffectTerrainDelete(effect.id);
  }

     
public void visitLevelEffect(ILevelEffect effect) { effect.visitILevelEffect(this); }
  public void visitLevelCreateEffect(LevelCreateEffect effect) {
    var instance = root.EffectLevelCreate(
  root.GetTerrain(effect.incarnation.terrain)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitLevelDeleteEffect(LevelDeleteEffect effect) {
    root.EffectLevelDelete(effect.id);
  }

     
public void visitRandEffect(IRandEffect effect) { effect.visitIRandEffect(this); }
  public void visitRandCreateEffect(RandCreateEffect effect) {
    var instance = root.EffectRandCreate(
  effect.incarnation.rand    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitRandDeleteEffect(RandDeleteEffect effect) {
    root.EffectRandDelete(effect.id);
  }

     
  public void visitRandSetRandEffect(RandSetRandEffect effect) {
    root.EffectRandSetRand(
      effect.id,
  effect.newValue
    );
  }

    public void visitStrMutListEffect(IStrMutListEffect effect) { effect.visitIStrMutListEffect(this); }
    public void visitStrMutListCreateEffect(StrMutListCreateEffect effect) {
      var list = root.EffectStrMutListCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
    }
    public void visitStrMutListDeleteEffect(StrMutListDeleteEffect effect) {
      root.EffectStrMutListDelete(effect.id);
    }
    public void visitStrMutListAddEffect(StrMutListAddEffect effect) {
      root.EffectStrMutListAdd(effect.id, effect.index, effect.element);
    }
    public void visitStrMutListRemoveEffect(StrMutListRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectStrMutListRemoveAt(effect.id, effect.index);
    }
       
    public void visitTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visitITerrainTileByLocationMutMapEffect(this); }
    public void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect) {
      var list = root.EffectTerrainTileByLocationMutMapCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
    }
    public void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect) {
      root.EffectTerrainTileByLocationMutMapDelete(effect.id);
    }
    public void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect) {
      root.EffectTerrainTileByLocationMutMapAdd(effect.id, effect.key, effect.value);
    }
    public void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTerrainTileByLocationMutMapRemove(effect.id, effect.key);
    }
     
  public void Apply(IEffect effect) {
    effect.visitIEffect(this);
  }
}
         
}
