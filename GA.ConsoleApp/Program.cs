using System;
using GA.Lib;

namespace GA.ConsoleApp
{
  public sealed class Program
  {
    static void Main(string[] args)
    {
        (new AlgorithmRunner()).Run(new AlgorithmRunnerOptions()
        {
          PopulationSize = 25,
          MaximumGenerations = 250,
          SurvivingPopulationPercentage = 0.5M,
          CrossoverChancePercentage = 0.25M,
          MutationChancePercentage = 0.05M,
          // StartingCity = City.CPT,
        });
    }
  }
}
