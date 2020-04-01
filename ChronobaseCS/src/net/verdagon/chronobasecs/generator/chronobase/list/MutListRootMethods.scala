package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutabilityS, MutableS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutListRootMethods {
  def generateRootMethods(opt: ChronobaseOptions, list: ListS): String = {
    val ListS(listName, MutableS, elementType) = list

    val elementCSType = toCS(elementType)
    val flattenedElementCSType = toCS(elementType.flatten)

    s"""
       |    public int Get${listName}Hash(int id, int version, ${listName}Incarnation incarnation) {
       |      int result = id * version;
       |      foreach (var element in incarnation.elements) {
       |        result += id * version * element.GetDeterministicHashCode();
       |      }
       |      return result;
       |    }
       |    public ${listName}Incarnation Get${listName}Incarnation(int id) {
       |      return rootIncarnation.incarnations${listName}[id].incarnation;
       |    }
       |    public ${listName} Get${listName}(int id) {
       |      CheckHas${listName}(id);
       |      return new ${listName}(this, id);
       |    }
       |    public ${listName} Get${listName}OrNull(int id) {
       |      if (${listName}Exists(id)) {
       |        return new ${listName}(this, id);
       |      } else {
       |        return new ${listName}(this, 0);
       |      }
       |    }
       |    public List<${listName}> All${listName}() {
       |      List<${listName}> result = new List<${listName}>(rootIncarnation.incarnations${listName}.Count);
       |      foreach (var id in rootIncarnation.incarnations${listName}.Keys) {
       |        result.Add(new ${listName}(this, id));
       |      }
       |      return result;
       |    }
       |    public bool ${listName}Exists(int id) {
       |      return rootIncarnation.incarnations${listName}.ContainsKey(id);
       |    }
       |    public void CheckHas${listName}(${listName} thing) {
       |      CheckRootsEqual(this, thing.root);
       |      CheckHas${listName}(thing.id);
       |    }
       |    public void CheckHas${listName}(int id) {
       |      if (!rootIncarnation.incarnations${listName}.ContainsKey(id)) {
       |        throw new System.Exception("Invalid ${listName}}: " + id);
       |      }
       |    }
       |    public ${listName} Effect${listName}Create() {
       |      return TrustedEffect${listName}CreateWithId(NewId());
       |    }
       |    public ${listName} TrustedEffect${listName}CreateWithId(int id) {
       |      CheckUnlocked();
       |      Asserts.Assert(!rootIncarnation.incarnations${listName}.ContainsKey(id));
       |      var effect = InternalEffectCreate${listName}(id, rootIncarnation.version, new ${listName}Incarnation(new List<${flattenedElementCSType}>()));
       |      NotifyEffect(effect);
       |      return new ${listName}(this, id);
       |    }
       |    public ${listName} Effect${listName}Create(IEnumerable<${elementCSType}> elements) {
       |      var list = Effect${listName}Create();
       |      foreach (var element in elements) {
       |        list.Add(element);
       |      }
       |      return list;
       |    }
       |""".stripMargin +
    s"""    public ${listName}CreateEffect InternalEffectCreate${listName}(int id, int incarnationVersion, ${listName}Incarnation incarnation) {
       |      rootIncarnation.incarnations${listName}
       |          .Add(
       |              id,
       |              new VersionAndIncarnation<${listName}Incarnation>(
       |                  incarnationVersion,
       |                  incarnation));
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash += Get${listName}Hash(id, incarnationVersion, incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      return new ${listName}CreateEffect(id);
       |    }
       |    public void Effect${listName}Delete(int id) {
       |      var effect = InternalEffect${listName}Delete(id);
       |      NotifyEffect(effect);
       |    }
       |    public ${listName}DeleteEffect InternalEffect${listName}Delete(int id) {
       |      CheckUnlocked();
       |      var versionAndIncarnation = rootIncarnation.incarnations${listName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${listName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${listName}.Remove(id);
       |      return new ${listName}DeleteEffect(id);
       |    }
       |    public void Effect${listName}Add(int instanceId, int addIndex, ${flattenedElementCSType} element) {
       |      CheckUnlocked();
       |      CheckHas${listName}(instanceId);
    """.stripMargin+
    (elementType.kind.mutability match {
      case MutableS => s"      CheckHas${elementCSType}(element);"
      case ImmutableS => ""
    }) +
    s"""
       |      var effect = InternalEffect${listName}Add(instanceId, addIndex, element);
       |      NotifyEffect(effect);
       |    }
       |    public ${listName}AddEffect InternalEffect${listName}Add(int instanceId, int addIndex, ${flattenedElementCSType} element) {
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[instanceId];
       |
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.elements.Insert(addIndex, element);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += instanceId * rootIncarnation.version * element.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.elements;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.Insert(addIndex, element);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[instanceId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(instanceId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(instanceId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      return new ${listName}AddEffect(instanceId, addIndex, element);
       |    }
       |    public void Effect${listName}RemoveAt(int instanceId, int index) {
       |      CheckUnlocked();
       |      CheckHas${listName}(instanceId);
       |      var effect = InternalEffect${listName}RemoveAt(instanceId, index);
       |      NotifyEffect(effect);
       |    }
       |    public ${listName}RemoveEffect InternalEffect${listName}RemoveAt(int instanceId, int index) {
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[instanceId];
       |      // Check that its there
       |      var oldElement = oldIncarnationAndVersion.incarnation.elements[index];
       |
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= instanceId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""        oldIncarnationAndVersion.incarnation.elements.RemoveAt(index);
       |      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.elements;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.RemoveAt(index);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[instanceId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(instanceId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(instanceId, rootIncarnation.version, newIncarnation);
           |""".stripMargin
      } else "") +
      s"""
       |      }
       |      return new ${listName}RemoveEffect(instanceId, index);
       |    }
       """.stripMargin
  }
}
