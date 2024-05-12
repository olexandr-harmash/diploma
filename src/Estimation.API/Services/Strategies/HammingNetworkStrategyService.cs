using diploma.Estimation.API.Services.Abstractions;

namespace Estimation.API.Services.Strategies;

public class HammingNetworkStrategyService : IStrategyService
{
    private const int T = 0; // Hopfield activate function constant, set to 0
    private const int MAX_ITER = 1400; // Maximum number of iterations

    private int _sizeLayer0; // Size of layer 0
    private int _sizeLayer1; // Size of layer 1

    private double _braking; // Braking parameter

    private double[] _layer0; // Array representing layer 0
    private double[] _layer1; // Array representing layer 1
    private double[] _layer1Prev; // Array representing the previous state of layer 1

    private double[][] _patterns; // 2D array representing patterns

    private double[,] _weightHamming; // 2D array representing Hamming weights

    public int GetZeroLayerSize()
    {
        return _sizeLayer0;
    }

    public int GetFirstLayerSize()
    {
        return _sizeLayer1;
    }

    public int GetCountOfPatterns()
    {
        return _sizeLayer1;
    }

    public void SetPatterns(double[][] patterns)
    {
        _patterns = patterns;
    }

    public Task Train()
    {
        _sizeLayer1 = _patterns.Count();
        _sizeLayer0 = _patterns.First().Count();

        _layer0 = new double[_sizeLayer0];
        _layer1 = new double[_sizeLayer1];

        _weightHamming = new double[_sizeLayer1, _sizeLayer0];

        _braking = 1.0 / (_sizeLayer1 * 2.0);

        for (int fi = 0; fi < _sizeLayer1; fi++)
        {
            _layer0 = _patterns[fi];
            for (int i = 0; i < _sizeLayer0; i++)
            {
                _weightHamming[fi, i] = _layer0[i] * 0.5;
            }
        }

        return Task.CompletedTask;
    }

    public Task<int> TestPattern(double[] pattern)
    {
        _layer0 = pattern;

        StepHamming();
        for (int i = 0; i < MAX_ITER; i++)
        {
            StepHopfield();
            if (StableHopfield())
                break;
        }

        var patternIndex = Array.IndexOf(_layer1, _layer1.Max());

        return Task.FromResult(patternIndex);
    }

    private bool StableHopfield()
    {
        return _layer1 == _layer1Prev;
    }

    private double HopfieldActivateFunction(double x)
    {
        return x > T ? x : T;
    }

    private double StateHopfield(int num)
    {
        double s = 0.0;
        for (int i = 0; i < _sizeLayer1; i++)
        {
            if (i != num)
            {
                s += _layer1Prev[i];
            }
        }
        return _layer1Prev[num] - _braking * s;
    }

    private void StepHopfield()
    {
        _layer1Prev = _layer1;
        for (int i = 0; i < _sizeLayer1; i++)
        {
            _layer1[i] = HopfieldActivateFunction(StateHopfield(i));
        }
    }

    private double StateHamming(int num)
    {
        double s = 0.0;
        for (int i = 0; i < _sizeLayer0; i++)
        {
            s += _layer0[i] * _weightHamming[num, i];
        }
        return s;
    }

    private void StepHamming()
    {
        for (int i = 0; i < _sizeLayer1; i++)
        {
            _layer1[i] = StateHamming(i);
        }
    }
}
