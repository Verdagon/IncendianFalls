package net.verdagon.chronobasecs.generator.chronobase.map

import net.verdagon.chronobasecs.compiled.{ImmutableS, MapS, MutabilityS, MutableS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutMapRootMethods {

  def generateRootMapMethods(opt: ChronobaseOptions, map: MapS): String = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val flattenedKeyCSType = toCS(keyType.flatten)
    val elementCSType = toCS(elementType)
    val flattenedElementCSType = toCS(elementType.flatten)

    s"""
       |    public int Get${mapName}Hash(int id, int version, ${mapName}Incarnation incarnation) {
       |      int result = id * version;
       |      foreach (var entry in incarnation.map) {
       |        result += id * version * entry.Key.GetDeterministicHashCode() * entry.Value.GetDeterministicHashCode();
       |      }
       |      return result;
       |    }
       |    public ${mapName}Incarnation Get${mapName}Incarnation(int id) {
       |      return rootIncarnation.incarnations${mapName}[id].incarnation;
       |    }
       |    public ${mapName} Get${mapName}(int id) {
       |      return new ${mapName}(this, id);
       |    }
       |    public List<${mapName}> All${mapName}() {
       |      List<${mapName}> result = new List<${mapName}>(rootIncarnation.incarnations${mapName}.Count);
       |      foreach (var id in rootIncarnation.incarnations${mapName}.Keys) {
       |        result.Add(new ${mapName}(this, id));
       |      }
       |      return result;
       |    }
       |    public bool ${mapName}Exists(int id) {
       |      return rootIncarnation.incarnations${mapName}.ContainsKey(id);
       |    }
       |    public void CheckHas${mapName}(${mapName} thing) {
       |      CheckRootsEqual(this, thing.root);
       |      CheckHas${mapName}(thing.id);
       |    }
       |    public void CheckHas${mapName}(int id) {
       |      if (!rootIncarnation.incarnations${mapName}.ContainsKey(id)) {
       |        throw new System.Exception("Invalid ${mapName}}: " + id);
       |      }
       |    }
       |    public ${mapName} Effect${mapName}Create() {
       |      CheckUnlocked();
       |      var id = NewId();
       |      EffectInternalCreate${mapName}(
       |          id,
       |          rootIncarnation.version,
       |          new ${mapName}Incarnation(
       |              new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>()));
       |      return new ${mapName}(this, id);
       |    }
       """.stripMargin +
    s"""
       |    public void EffectInternalCreate${mapName}(int id, int incarnationVersion, ${mapName}Incarnation incarnation) {
       |      var effect = new ${mapName}CreateEffect(id);
       |      rootIncarnation.incarnations${mapName}
       |          .Add(
       |              id,
       |              new VersionAndIncarnation<${mapName}Incarnation>(
       |                  incarnationVersion,
       |                  incarnation));
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash += Get${mapName}Hash(id, incarnationVersion, incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      effects${mapName}CreateEffect.Add(effect);
       |    }
       |    public void Effect${mapName}Delete(int id) {
       |      CheckUnlocked();
       |      var effect = new ${mapName}DeleteEffect(id);
       |      effects${mapName}DeleteEffect.Add(effect);
       |      var versionAndIncarnation = rootIncarnation.incarnations${mapName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${mapName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${mapName}.Remove(id);
       |    }
       |    public void Effect${mapName}Add(int mapId, ${flattenedKeyCSType} key, ${flattenedElementCSType} value) {
       |      CheckUnlocked();
       |      CheckHas${mapName}(mapId);
       |      CheckHas${elementCSType}(value);
       |
       |      var effect = new ${mapName}AddEffect(mapId, key, value);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${mapName}[mapId];
       |      if (oldIncarnationAndVersion.incarnation.map.ContainsKey(key)) {
       |        throw new Exception("Key exists! " + key);
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.map.Add(key, value);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += mapId * rootIncarnation.version * key.GetDeterministicHashCode() * value.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.map;
       |        var newMap = new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(oldMap);
       |        newMap.Add(key, value);
       |        var newIncarnation = new ${mapName}Incarnation(newMap);
       |        rootIncarnation.incarnations${mapName}[mapId] =
       |            new VersionAndIncarnation<${mapName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${mapName}Hash(mapId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${mapName}Hash(mapId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      effects${mapName}AddEffect.Add(effect);
       |    }
       """.stripMargin +
    s"""
       |    public void Effect${mapName}Remove(int mapId, ${flattenedKeyCSType} key) {
       |      CheckUnlocked();
       |      CheckHas${mapName}(mapId);
       |
       |      var effect = new ${mapName}RemoveEffect(mapId, key);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${mapName}[mapId];
       |      if (!oldIncarnationAndVersion.incarnation.map.ContainsKey(key)) {
       |        throw new Exception("Key doesnt exist! " + key);
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        var oldValue = oldIncarnationAndVersion.incarnation.map[key];
       |        oldIncarnationAndVersion.incarnation.map.Remove(key);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= mapId * rootIncarnation.version * key.GetDeterministicHashCode() * oldValue.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.map;
       |        var newMap = new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(oldMap);
       |        newMap.Remove(key);
       |        var newIncarnation = new ${mapName}Incarnation(newMap);
       |        rootIncarnation.incarnations${mapName}[mapId] =
       |            new VersionAndIncarnation<${mapName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${mapName}Hash(mapId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${mapName}Hash(mapId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      effects${mapName}RemoveEffect.Add(effect);
       |    }
       |    public void Add${mapName}Observer(int id, I${mapName}EffectObserver observer) {
       |      List<I${mapName}EffectObserver> obsies;
       |      if (!observersFor${mapName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${mapName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${mapName}[id] = obsies;
       |    }
       |""".stripMargin +
    s"""
       |    public void Remove${mapName}Observer(int id, I${mapName}EffectObserver observer) {
       |      if (observersFor${mapName}.ContainsKey(id)) {
       |        var map = observersFor${mapName}[id];
       |        map.Remove(observer);
       |        if (map.Count == 0) {
       |          observersFor${mapName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       |""".stripMargin +
    generateBroadcastings(map)
  }

  def generateBroadcastings(map: MapS): String = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val mapCSType = toCS(map.tyype)

    val observerName = s"I${mapName}EffectObserver"
    val createEffectName = s"${mapName}CreateEffect"
    val deleteEffectName = s"${mapName}DeleteEffect"
    val addEffectName = s"${mapName}AddEffect"
    val removeEffectName = s"${mapName}RemoveEffect"

    // Delete has to be first. This is so it can clear away all those
    // observers observing this object, so they don't have to remove
    // themselves, and if something is ressurrected via revert, the
    // observers for the old existence won't be notified.
    s"""
       |  public void Broadcast${mapCSType}Effects(
       |      SortedDictionary<int, List<I${mapCSType}EffectObserver>> observers) {
       |    foreach (var effect in effects${deleteEffectName}) {
       |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
       |        foreach (var observer in globalObservers) {
       |          observer.On${mapCSType}Effect(effect);
       |        }
       |      }
       |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
       |        foreach (var observer in objObservers) {
       |          observer.On${mapCSType}Effect(effect);
       |        }
       |        observersFor${mapCSType}.Remove(effect.id);
       |      }
       |    }
       |    effects${deleteEffectName}.Clear();
       |""".stripMargin +
    List(addEffectName, removeEffectName, createEffectName)
      .map(effectCSType => {
        s"""
           |    foreach (var effect in effects${effectCSType}) {
           |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
           |        foreach (var observer in globalObservers) {
           |          observer.On${mapCSType}Effect(effect);
           |        }
           |      }
           |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
           |        foreach (var observer in objObservers) {
           |          observer.On${mapCSType}Effect(effect);
           |        }
           |      }
           |    }
           |    effects${effectCSType}.Clear();
           |""".stripMargin
      })
      .mkString("") +
    s"""
       |  }
       |""".stripMargin
  }
}
