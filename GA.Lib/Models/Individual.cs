using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.Lib.Models
{
  public class Individual<T>
  {
    public IEnumerable<T> Chromosome { get; set; } = Enumerable.Empty<T>();

    public int Generation { get; set; }

    public int? Fitness { get; set; }

    public override string ToString() => string.Join(' ', this.Chromosome.Select(gene => gene?.ToString()));
  }
}
