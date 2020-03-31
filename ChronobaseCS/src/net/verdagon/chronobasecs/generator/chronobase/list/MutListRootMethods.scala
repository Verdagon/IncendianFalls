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
       |      foreach (var element in incarnation.list) {
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
       |      EffectInternalCreate${listName}(id, rootIncarnation.version, new ${listName}Incarnation(new List<${flattenedElementCSType}>()));
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
    s"""    public void EffectInternalCreate${listName}(int id, int incarnationVersion, ${listName}Incarnation incarnation) {
       |      var effect = new ${listName}CreateEffect(id);
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
      s"""      NotifyEffect(effect);
       |    }
       |    public void Effect${listName}Delete(int id) {
       |      CheckUnlocked();
       |      var effect = new ${listName}DeleteEffect(id);
       |      NotifyEffect(effect);
       |      var versionAndIncarnation = rootIncarnation.incarnations${listName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${listName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${listName}.Remove(id);
       |    }
       |    public void Effect${listName}Add(int listId, int addIndex, ${flattenedElementCSType} element) {
       |      CheckUnlocked();
       |      CheckHas${listName}(listId);
       |
    """.stripMargin+
    (elementType.kind.mutability match {
      case MutableS => s"      CheckHas${elementCSType}(element);"
      case ImmutableS => ""
    }) +
    s"""
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[listId];
       |      var effect = new ${listName}AddEffect(listId, addIndex, element);
       |
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.list.Insert(addIndex, element);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.list;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.Insert(addIndex, element);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[listId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(listId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      NotifyEffect(effect);
       |    }
       |    public void Effect${listName}RemoveAt(int listId, int index) {
       |      CheckUnlocked();
       |      CheckHas${listName}(listId);
       |
       |      var effect = new ${listName}RemoveEffect(listId, index);
       |
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[listId];
       |      // Check that its there
       |      var oldElement = oldIncarnationAndVersion.incarnation.list[index];
       |
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
       |      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.list;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.RemoveAt(index);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[listId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(listId, rootIncarnation.version, newIncarnation);
           |""".stripMargin
      } else "") +
      s"""
       |      }
       |      NotifyEffect(effect);
       |    }
       """.stripMargin
  }
}
