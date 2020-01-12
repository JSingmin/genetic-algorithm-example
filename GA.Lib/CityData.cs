using System;
using System.Collections.Generic;

namespace GA.Lib
{
  public enum City
  {
    None = 0,
    CPT = 1, // Cape Town
    JNB = 2, // Johnnesburg
    PLZ = 3, // Port Elizabeth
    PTA = 4, // Pretoria
    DUR = 5, // Durban
    ELS = 6, // East London
    GRJ = 7, // George
    BFN = 8, // Bloemfontein
    KIM = 9, // Kimberley
    UTN = 10, // Upington
  }

  public class CityData
  {
    public IDictionary<City, int> CPT { get; } = new Dictionary<City, int>
    {
      { City.JNB, 1398 },
      { City.PLZ, 749 },
      { City.PTA, 1461 },
      { City.DUR, 1635 },
      { City.ELS, 1028 },
      { City.GRJ, 428 },
      { City.BFN, 1005 },
      { City.KIM, 986 },
      { City.UTN, 836 },
    };

    public IDictionary<City, int> JNB { get; } = new Dictionary<City, int>
    {
      { City.CPT, 1398 },
      { City.PLZ, 1045 },
      { City.PTA, 63 },
      { City.DUR, 568 },
      { City.ELS, 938 },
      { City.GRJ, 1168 },
      { City.BFN, 398 },
      { City.KIM, 498 },
      { City.UTN, 794 },
    };

    public IDictionary<City, int> PLZ { get; } = new Dictionary<City, int>
    {
      { City.CPT, 749 },
      { City.JNB, 1045 },
      { City.PTA, 1110 },
      { City.DUR, 910 },
      { City.ELS, 284 },
      { City.GRJ, 331 },
      { City.BFN, 654 },
      { City.KIM, 714 },
      { City.UTN, 926 },
    };

    public IDictionary<City, int> PTA { get; } = new Dictionary<City, int>
    {
      { City.CPT, 1461 },
      { City.JNB, 63 },
      { City.PLZ, 1110 },
      { City.DUR, 618 },
      { City.ELS, 995 },
      { City.GRJ, 1225 },
      { City.BFN, 455 },
      { City.KIM, 554 },
      { City.UTN, 828 },
    };

    public IDictionary<City, int> DUR { get; } = new Dictionary<City, int>
    {
      { City.CPT, 1635 },
      { City.JNB, 568 },
      { City.PLZ, 910 },
      { City.PTA, 618 },
      { City.ELS, 658 },
      { City.GRJ, 1283 },
      { City.BFN, 635 },
      { City.KIM, 795 },
      { City.UTN, 1201 },
    };

    public IDictionary<City, int> ELS { get; } = new Dictionary<City, int>
    {
      { City.CPT, 1028 },
      { City.JNB, 938 },
      { City.PLZ, 284 },
      { City.PTA, 995 },
      { City.DUR, 658 },
      { City.GRJ, 610 },
      { City.BFN, 569 },
      { City.KIM, 722 },
      { City.UTN, 960 },
    };

    public IDictionary<City, int> GRJ { get; } = new Dictionary<City, int>
    {
      { City.CPT, 428 },
      { City.JNB, 1168 },
      { City.PLZ, 331 },
      { City.PTA, 1225 },
      { City.DUR, 1283 },
      { City.ELS, 610 },
      { City.BFN, 775 },
      { City.KIM, 724 },
      { City.UTN, 854 },
    };

    public IDictionary<City, int> BFN { get; } = new Dictionary<City, int>
    {
      { City.CPT, 1005 },
      { City.JNB, 398 },
      { City.PLZ, 654 },
      { City.PTA, 455 },
      { City.DUR, 635 },
      { City.ELS, 569 },
      { City.GRJ, 775 },
      { City.KIM, 165 },
      { City.UTN, 571 },
    };

    public IDictionary<City, int> KIM { get; } = new Dictionary<City, int>
    {
      { City.CPT, 986 },
      { City.JNB, 498 },
      { City.PLZ, 714 },
      { City.PTA, 554 },
      { City.DUR, 795 },
      { City.ELS, 722 },
      { City.GRJ, 724 },
      { City.BFN, 165 },
      { City.UTN, 409 },
    };

    public IDictionary<City, int> UTN { get; } = new Dictionary<City, int>
    {
      { City.CPT, 836 },
      { City.JNB, 794 },
      { City.PLZ, 926 },
      { City.PTA, 828 },
      { City.DUR, 1201 },
      { City.ELS, 960 },
      { City.GRJ, 854 },
      { City.BFN, 571 },
      { City.KIM, 409 },
    };

    public ISet<City> Cities { get; } = new HashSet<City>
    {
      City.CPT,
      City.JNB,
      City.PLZ,
      City.PTA,
      City.DUR,
      City.ELS,
      City.GRJ,
      City.BFN,
      City.KIM,
      City.UTN,
    };

    public IDictionary<City, int> this[City currentCity]
    {
      get
      {
        switch (currentCity)
        {
          case City.CPT:
            return this.CPT;
          case City.JNB:
            return this.JNB;
          case City.PLZ:
            return this.PLZ;
          case City.PTA:
            return this.PTA;
          case City.DUR:
            return this.DUR;
          case City.ELS:
            return this.ELS;
          case City.GRJ:
            return this.GRJ;
          case City.BFN:
            return this.BFN;
          case City.KIM:
            return this.KIM;
          case City.UTN:
            return this.UTN;
          default:
            throw new KeyNotFoundException();
        }
      }
    }
  }
}
