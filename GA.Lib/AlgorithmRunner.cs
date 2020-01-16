using System;
using System.Collections.Generic;
using System.Linq;
using GA.Lib.Models;
using GA.Lib.OutputWriters;

namespace GA.Lib
{
  public class AlgorithmRunner
  {
    private IOutputWriter OutputWriter { get; set; } = new ConsoleOutputWriter(debugMode: false);
    private CityData CityData { get; } = new CityData();

    public void Run(AlgorithmRunnerOptions options)
    {
      var population = this.GenerateInitialPopulation(options.PopulationSize, options.StartingCity);
      int generation = 1;
      bool canContinue = true;

      do
      {
        this.OutputWriter.WriteLine($"Generation {generation}");
        foreach (var individual in population)
        {
          if (!individual.Fitness.HasValue)
          {
            individual.Fitness = this.CalculateFitness(individual);
          }

          this.OutputWriter.Debug($"{individual.ToString()} -> {individual.Fitness}");
        }

        var fittest = population.OrderBy(i => i.Fitness).First();
        var fittestPopulation = population.Where(i => i.Fitness == fittest.Fitness);
        foreach (Individual<City> fitIndividual in fittestPopulation)
        {
          this.OutputWriter.WriteLine($"{fitIndividual.ToString()} -> {fitIndividual.Fitness}");
        }
        this.OutputWriter.WriteLine("");
        generation++;

        canContinue = options.MaximumGenerations == 0 || generation <= options.MaximumGenerations;
        if (canContinue)
        {

          population = this.GenerateNextPopulation(
            population,
            generation,
            options.StartingCity,
            options.CrossoverChancePercentage,
            options.MutationChancePercentage
          );
        }
      } while (canContinue);
    }

    private IEnumerable<Individual<City>> GenerateInitialPopulation(int populationSize, City startingCity)
    {
      this.OutputWriter.WriteLine($"Generating Initial Population (Size: {populationSize})");

      var initialPopulation = new List<Individual<City>>(populationSize);
      for (int i = 0; i < populationSize; i++)
      {
        var random = new Random();
        var cities = this.CityData.Cities
          .Where(c => c != startingCity)
          .Select(c => new { Value = c, Order = random.Next() })
          .OrderBy(c => c.Order)
          .Select(c => c.Value)
          .ToList();

        initialPopulation.Add(new Individual<City>
        {
          Chromosome = this.CityData.Cities.Contains(startingCity)
            ? cities.Prepend(startingCity)
            : cities,
          Generation = 1,
        });
      }

      this.OutputWriter.Debug("Generating Initial Population Results:");
      initialPopulation.ForEach(individual => this.OutputWriter.Debug(individual.ToString()));
      this.OutputWriter.Debug($"Total Initial Population: {initialPopulation.Count}");
      this.OutputWriter.WriteLine("");
      return initialPopulation;
    }

    private int CalculateFitness(Individual<City> individual)
    {
      int edges = individual.Chromosome.Count() - 1;
      int totalTravelDistance = 0;
      for (int i = 0; i < edges; i++)
      {
        City currentCity = individual.Chromosome.ElementAt(i);
        City nextCity = individual.Chromosome.ElementAt(i + 1);
        totalTravelDistance += this.CityData[currentCity][nextCity];
      }

      totalTravelDistance += this.CityData[individual.Chromosome.ElementAt(edges)][individual.Chromosome.ElementAt(0)];
      return totalTravelDistance;
    }

    private IEnumerable<Individual<City>> GenerateNextPopulation(
      IEnumerable<Individual<City>> population,
      int generation,
      City startingCity,
      decimal crossoverChancePercentage,
      decimal mutationChancePercentage
    )
    {
      var rankedPopulation = this.GetRankedPopulation(population);
      int populationSize = rankedPopulation.Count();
      int totalRankingNumbers = (populationSize + 1) * populationSize / 2;

      var nextGeneration = new List<Individual<City>>();
      for (int i = 0; i < populationSize; i++)
      {
        var random = new Random();
        var selectedIndividual = this.SelectIndividual(rankedPopulation, random, totalRankingNumbers);
        var newIndividual = new Individual<City>
        {
          Chromosome = new List<City>(selectedIndividual.Chromosome),
          Generation = selectedIndividual.Generation,
        };

        if ((decimal)random.NextDouble() <= crossoverChancePercentage)
        {
          var otherSelectedIndividual = this.SelectIndividual(rankedPopulation, random, totalRankingNumbers);
          newIndividual = this.Crossover(selectedIndividual, otherSelectedIndividual, random, generation, startingCity);
        }

        if ((decimal)random.NextDouble() <= mutationChancePercentage)
        {
          newIndividual = this.Mutate(newIndividual, random, generation, startingCity);
        }

        nextGeneration.Add(newIndividual);
      }

      return nextGeneration;
    }

    private IDictionary<Range, Individual<City>> GetRankedPopulation(IEnumerable<Individual<City>> population)
    {
      int populationSize = population.Count();
      var rankedPopulation = population.OrderByDescending(i => i.Fitness);
      var rangeRankedPopulation = new Dictionary<Range, Individual<City>>(populationSize);
      int rankRangeCounter = 1;

      for (int i = 0; i < populationSize; i++)
      {
        rangeRankedPopulation.Add(rankRangeCounter..(rankRangeCounter + i), rankedPopulation.ElementAt(i));
        rankRangeCounter += i + 1;
      }

      return rangeRankedPopulation;
    }

    private Individual<City> SelectIndividual(IDictionary<Range, Individual<City>> rankedPopulation, Random random, int totalRankingNumbers)
    {
        int selectedRangedRank = random.Next(totalRankingNumbers) + 1;
        return rankedPopulation
          .First(i => selectedRangedRank >= i.Key.Start.Value && selectedRangedRank <= i.Key.End.Value)
          .Value;
    }

    private Individual<City> Crossover(Individual<City> parent1, Individual<City> parent2, Random random, int generation, City startingCity)
    {
      var parent1Chromosome = parent1.Chromosome
        .Where(g => g != startingCity)
        .Take(random.Next(1, parent1.Chromosome.Count()));
      var parent2Chromosome = parent2.Chromosome
        .Where(g => g != startingCity && !parent1Chromosome.Contains(g));

      var newChromosome = new List<City>(this.CityData.Cities.Count);
      if (random.NextDouble() < 0.5)
      {
        newChromosome.InsertRange(0, parent1Chromosome);
        newChromosome.InsertRange(0, parent2Chromosome);
      }
      else
      {
        newChromosome.InsertRange(0, parent2Chromosome);
        newChromosome.InsertRange(0, parent1Chromosome);
      }

      return new Individual<City>
      {
        Chromosome = this.CityData.Cities.Contains(startingCity)
          ? newChromosome.Prepend(startingCity)
          : newChromosome,
        Generation = generation,
      };
    }

    private Individual<City> Mutate(Individual<City> individual, Random random, int generation, City startingCity)
    {
      int startingRandom = this.CityData.Cities.Contains(startingCity) ? 1 : 0;
      var mutatedChromosome = new List<City>(individual.Chromosome);
      int switch1Index = random.Next(startingRandom, individual.Chromosome.Count());
      int switch2Index = random.Next(startingRandom, individual.Chromosome.Count());
      while (switch1Index == switch2Index)
      {
        switch2Index = random.Next(startingRandom, individual.Chromosome.Count());
      }

      City temp = mutatedChromosome[switch1Index];
      mutatedChromosome[switch1Index] = mutatedChromosome[switch2Index];
      mutatedChromosome[switch2Index] = temp;

      return new Individual<City>
      {
        Chromosome = mutatedChromosome,
        Generation = generation,
      };
    }
  }
}
