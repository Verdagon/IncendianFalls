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
       |      foreach (var entry in incarnation.elements) {
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
       |      return TrustedEffect${mapName}CreateWithId(NewId());
       |    }
       |    public ${mapName} TrustedEffect${mapName}CreateWithId(int id) {
       |      CheckUnlocked();
       |      Asserts.Assert(!rootIncarnation.incarnations${mapName}.ContainsKey(id));
       |      var effect =
       |        InternalEffectCreate${mapName}(
       |          id,
       |          rootIncarnation.version,
       |          new ${mapName}Incarnation(
       |              new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>()));
       |      NotifyEffect(effect);
       |      return new ${mapName}(this, id);
       |    }
       """.stripMargin +
    s"""
       |    public ${mapName}CreateEffect InternalEffectCreate${mapName}(int id, int incarnationVersion, ${mapName}Incarnation incarnation) {
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
      s"""    return new ${mapName}CreateEffect(id);
       |    }
       |    public void Effect${mapName}Delete(int id) {
       |      var effect = InternalEffect${mapName}Delete(id);
       |      NotifyEffect(effect);
       |    }
       |    public ${mapName}DeleteEffect InternalEffect${mapName}Delete(int id) {
       |      CheckUnlocked();
       |      var versionAndIncarnation = rootIncarnation.incarnations${mapName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${mapName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${mapName}.Remove(id);
       |        return new ${mapName}DeleteEffect(id);
       |    }
       |    public void Effect${mapName}Add(int instanceId, ${flattenedKeyCSType} key, ${flattenedElementCSType} value) {
       |      CheckUnlocked();
       |      CheckHas${mapName}(instanceId);
       |      CheckHas${elementCSType}(value);
       |      var effect = InternalEffect${mapName}Add(instanceId, key, value);
       |      NotifyEffect(effect);
       |    }
       |    public ${mapName}AddEffect InternalEffect${mapName}Add(int instanceId, ${flattenedKeyCSType} key, ${flattenedElementCSType} value) {
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${mapName}[instanceId];
       |      if (oldIncarnationAndVersion.incarnation.elements.ContainsKey(key)) {
       |        throw new Exception("Key exists! " + key);
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.elements.Add(key, value);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += instanceId * rootIncarnation.version * key.GetDeterministicHashCode() * value.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.elements;
       |        var newMap = new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(oldMap);
       |        newMap.Add(key, value);
       |        var newIncarnation = new ${mapName}Incarnation(newMap);
       |        rootIncarnation.incarnations${mapName}[instanceId] =
       |            new VersionAndIncarnation<${mapName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${mapName}Hash(instanceId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${mapName}Hash(instanceId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      return new ${mapName}AddEffect(instanceId, key, value);
       |    }
       """.stripMargin +
    s"""
       |    public void Effect${mapName}Remove(int instanceId, ${flattenedKeyCSType} key) {
       |      CheckUnlocked();
       |      CheckHas${mapName}(instanceId);
       |      var effect = InternalEffect${mapName}Remove(instanceId, key);
       |      NotifyEffect(effect);
       |    }
       |    public ${mapName}RemoveEffect InternalEffect${mapName}Remove(int instanceId, ${flattenedKeyCSType} key) {
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${mapName}[instanceId];
       |      if (!oldIncarnationAndVersion.incarnation.elements.ContainsKey(key)) {
       |        throw new Exception("Key doesnt exist! " + key);
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        var oldValue = oldIncarnationAndVersion.incarnation.elements[key];
       |        oldIncarnationAndVersion.incarnation.elements.Remove(key);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= instanceId * rootIncarnation.version * key.GetDeterministicHashCode() * oldValue.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.elements;
       |        var newMap = new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(oldMap);
       |        newMap.Remove(key);
       |        var newIncarnation = new ${mapName}Incarnation(newMap);
       |        rootIncarnation.incarnations${mapName}[instanceId] =
       |            new VersionAndIncarnation<${mapName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${mapName}Hash(instanceId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${mapName}Hash(instanceId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      return new ${mapName}RemoveEffect(instanceId, key);
       |    }
       |""".stripMargin
  }
}
