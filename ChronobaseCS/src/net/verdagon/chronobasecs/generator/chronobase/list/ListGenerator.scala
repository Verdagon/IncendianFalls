package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutableS, StructS}
import net.verdagon.chronobasecs.generator.MutStructImpl
import net.verdagon.chronobasecs.generator.chronobase.struct.MutStructRootMethods
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object ListGenerator {
  def generateHandles(opt: ChronobaseOptions, list: ListS): Map[String, String] = {
    list.mutability match {
      case MutableS => {
        MutListHandle.generateHandle(opt, list) ++
        MutListIncarnation.generateIncarnation(opt, list) ++
        MutListEffects.generateEffects(opt, list)
      }
      case ImmutableS => {
        ImmList.generateImmList(opt, list)
      }
    }
  }

  def generateRootMethods(opt: ChronobaseOptions, list: ListS): String = {
    list.mutability match {
      case MutableS => {
        MutListRootMethods.generateRootMethods(opt, list)
      }
      case ImmutableS => ""
    }
  }

  def generateRootMembers(opt: ChronobaseOptions, list: ListS): String = {
    val structCSType = toCS(list.tyype)
    s"""
       |  readonly SortedDictionary<int, List<I${structCSType}EffectObserver>> observersFor${structCSType} =
       |      new SortedDictionary<int, List<I${structCSType}EffectObserver>>();
       |""".stripMargin +
      MutListEffects.generateRootMembers(opt, list)
  }
}
