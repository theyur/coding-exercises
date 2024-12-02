using System.Runtime.CompilerServices;

namespace BikersAndBikes;

public class Result
{

    /*
     * Complete the 'bikeRacers' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts following parameters:
     *  1. 2D_INTEGER_ARRAY bikers
     *  2. 2D_INTEGER_ARRAY bikes
     */
    
    private const long Magnitude = 1_000_000;
    private static int _globalK;
    
    private static ulong _methodCalls = 0;

    private static Dictionary<(long, long), List<(double distance, long biker, long bike)>> _memo = [];
    
    private static double _distanceOfLevelK = double.MaxValue;
    
    private static (double distance, long biker, long bike) _initialLimiter;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void UpdateDistanceIfMin(double newDistance) => _distanceOfLevelK = Math.Min(_distanceOfLevelK, newDistance);
    
    // State is coordinates hash x * Magnitude + y
    private static void GetMinDistance( 
        List<(double distance, long biker, long bike)> orderedDistances,
        int k,
        bool firstRun)
    {
        _methodCalls++;
        if (_methodCalls % 1_000_000 == 0) Console.WriteLine($"Method calls: {_methodCalls}");

        if (k == _globalK)
        {
            var (distance, biker, bike) = orderedDistances.FirstOrDefault();
            if (firstRun) _initialLimiter = (distance, biker, bike);
            
            if (biker is not 0 && bike is not 0) UpdateDistanceIfMin(distance);
            return;
        }

        foreach (var ((distance, biker, bike), i) in orderedDistances.Take(orderedDistances.Count - _globalK + k).Select((t, i) => (t, i)))
        {
            var currentMinOnLevelK = _distanceOfLevelK;

            if (distance > currentMinOnLevelK) break;

            var valueTuples = orderedDistances;
            if (orderedDistances.Count > 1)
            {
                if (!_memo.TryGetValue((biker, bike), out valueTuples))
                {
                    valueTuples = orderedDistances
                        .Skip(i + 1)
                        //.SkipWhile(x => x.biker == biker || x.bike == bike)
                        .Where(x => x.biker != biker && x.bike != bike)
                        .ToList();
                   _memo[(biker, bike)] = valueTuples;
                }
            }

            if (valueTuples.Count > 0) GetMinDistance(valueTuples, k + 1, firstRun);

            if (firstRun) break;
        }
    }
    
    public static long bikeRacers(List<List<int>> bikers, List<List<int>> bikes, int k)
    {
        double Distance((int x,int y) biker, (int x,int y) bike)
        {
            long diffX = Math.Abs(biker.x - bike.x);
            long diffY = Math.Abs(biker.y - bike.y);
            
            return Math.Sqrt(diffX*diffX + diffY*diffY);
        }

        _globalK = k;
        
        var distances = new List<(double d, long biker, long bike)>();
        foreach (var (biker, i) in bikers.Select((b, i) => (b, i)))
        foreach (var (bike, j) in bikes.Select((b, j) => (b, j)))
            distances.Add(
                (Distance((biker[0], biker[1]), (bike[0], bike[1])),
                    biker[0] * Magnitude + biker[1], bike[0] * Magnitude + bike[1])
            );

        // Print top 20 distances for debugging
        distances.OrderBy(x => x.d).Take(20).ToList()
            .ForEach(d =>
            {
                Console.WriteLine(d
                    // + "\t"
                    // + d.biker / Magnitude + " " + d.biker % Magnitude
                    // + ", \t"
                    // + d.bike / Magnitude + " " + d.bike % Magnitude
                );
            });

        var orderedDistances = distances.OrderBy(x => x.d).ToList();
        
        var counter = 0;
        var index = 0;
        
        List<long> bikersIds, bikesIds;
        bikersIds = [];
        bikesIds = [];

        foreach (var ((_, biker, bike), i) in orderedDistances.Select((t, i) => (t, i)))
        {
            var found = false;
            
            if (!bikersIds.Contains(biker))
            {
                bikersIds.Add(biker);
                found = true;
            }
            
            if (found || !bikesIds.Contains(bike))
            {
                bikesIds.Add(bike);
                found = true;
            }
            
            if (!found) counter++;
            
            if (counter == k)
            {
                index = i;
                break;
            }
        }
        
        var element = orderedDistances[index];
        
        return (long)Math.Round(element.d * element.d);
        
        GetMinDistance(orderedDistances, 1, true);
        
        var reducedDistances = orderedDistances
            .TakeWhile(x => x.biker != _initialLimiter.biker && x.bike != _initialLimiter.bike)
            .ToList();
        
        reducedDistances.Add(_initialLimiter);
        
        GetMinDistance(reducedDistances, 1, false);

        var result = _distanceOfLevelK;
        return (long)Math.Round(result * result);
    }
}
