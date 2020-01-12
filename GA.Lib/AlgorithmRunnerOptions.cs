using System;

namespace GA.Lib
{
  public struct AlgorithmRunnerOptions
  {
    public int PopulationSize { get; set; }
    public int MaximumGenerations { get; set; }
    public decimal SurvivingPopulationPercentage { get; set; }
    public decimal CrossoverChancePercentage { get; set; }
    public decimal MutationChancePercentage { get; set; }
    public City StartingCity { get; set; }
  }
}
