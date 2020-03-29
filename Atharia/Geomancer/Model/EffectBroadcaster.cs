using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class EffectBroadcaster : IEffectVisitor {

  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersForTerrainTile =
      new SortedDictionary<int, List<ITerrainTileEffectObserver>>();

  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersForTerrain =
      new SortedDictionary<int, List<ITerrainEffectObserver>>();

  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersForLevel =
      new SortedDictionary<int, List<ILevelEffectObserver>>();

  readonly SortedDictionary<int, List<IRandEffectObserver>> observersForRand =
      new SortedDictionary<int, List<IRandEffectObserver>>();

  readonly SortedDictionary<int, List<IStrMutListEffectObserver>> observersForStrMutList =
      new SortedDictionary<int, List<IStrMutListEffectObserver>>();

  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersForTerrainTileByLocationMutMap =
      new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();

  public EffectBroadcaster() {
  }


  public void visitTerrainTileEffect(ITerrainTileEffect effect) {
    if (observersForTerrainTile.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainTileEffectObserver>(observers)) {
        observer.OnTerrainTileEffect(effect);
      }
    }
  }
  public void AddTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    List<ITerrainTileEffectObserver> obsies;
    if (!observersForTerrainTile.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainTileEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrainTile[id] = obsies;
  }

  public void RemoveTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    if (observersForTerrainTile.ContainsKey(id)) {
      var list = observersForTerrainTile[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrainTile.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTerrainEffect(ITerrainEffect effect) {
    if (observersForTerrain.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainEffectObserver>(observers)) {
        observer.OnTerrainEffect(effect);
      }
    }
  }
  public void AddTerrainObserver(int id, ITerrainEffectObserver observer) {
    List<ITerrainEffectObserver> obsies;
    if (!observersForTerrain.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrain[id] = obsies;
  }

  public void RemoveTerrainObserver(int id, ITerrainEffectObserver observer) {
    if (observersForTerrain.ContainsKey(id)) {
      var list = observersForTerrain[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrain.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLevelEffect(ILevelEffect effect) {
    if (observersForLevel.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILevelEffectObserver>(observers)) {
        observer.OnLevelEffect(effect);
      }
    }
  }
  public void AddLevelObserver(int id, ILevelEffectObserver observer) {
    List<ILevelEffectObserver> obsies;
    if (!observersForLevel.TryGetValue(id, out obsies)) {
      obsies = new List<ILevelEffectObserver>();
    }
    obsies.Add(observer);
    observersForLevel[id] = obsies;
  }

  public void RemoveLevelObserver(int id, ILevelEffectObserver observer) {
    if (observersForLevel.ContainsKey(id)) {
      var list = observersForLevel[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLevel.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitRandEffect(IRandEffect effect) {
    if (observersForRand.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRandEffectObserver>(observers)) {
        observer.OnRandEffect(effect);
      }
    }
  }
  public void AddRandObserver(int id, IRandEffectObserver observer) {
    List<IRandEffectObserver> obsies;
    if (!observersForRand.TryGetValue(id, out obsies)) {
      obsies = new List<IRandEffectObserver>();
    }
    obsies.Add(observer);
    observersForRand[id] = obsies;
  }

  public void RemoveRandObserver(int id, IRandEffectObserver observer) {
    if (observersForRand.ContainsKey(id)) {
      var list = observersForRand[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRand.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

    public void visitStrMutListEffect(IStrMutListEffect effect) {
      if (observersForStrMutList.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IStrMutListEffectObserver>(observers)) {
          observer.OnStrMutListEffect(effect);
        }
      }
    }
    public void AddStrMutListObserver(int id, IStrMutListEffectObserver observer) {
      List<IStrMutListEffectObserver> obsies;
      if (!observersForStrMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IStrMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForStrMutList[id] = obsies;
    }
    public void RemoveStrMutListObserver(int id, IStrMutListEffectObserver observer) {
      if (observersForStrMutList.ContainsKey(id)) {
        var map = observersForStrMutList[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForStrMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void visitTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) {
    if (observersForTerrainTileByLocationMutMap.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainTileByLocationMutMapEffectObserver>(observers)) {
        observer.OnTerrainTileByLocationMutMapEffect(effect);
      }
    }
  }
    public void AddTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      List<ITerrainTileByLocationMutMapEffectObserver> obsies;
      if (!observersForTerrainTileByLocationMutMap.TryGetValue(id, out obsies)) {
        obsies = new List<ITerrainTileByLocationMutMapEffectObserver>();
      }
      obsies.Add(observer);
      observersForTerrainTileByLocationMutMap[id] = obsies;
    }
    public void RemoveTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      if (observersForTerrainTileByLocationMutMap.ContainsKey(id)) {
        var map = observersForTerrainTileByLocationMutMap[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForTerrainTileByLocationMutMap.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void Route(IEffect effect) {
    effect.visitIEffect(this);
  }
}
         
}
