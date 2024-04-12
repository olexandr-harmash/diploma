namespace diploma.Estimation.API.Services;

public class EstimationServiceBase
{
    protected const int T = 0;
    protected const int MAX_ITER = 1400;
    protected int _sizeLayer0;
    protected int _sizeLayer1;
    private double _braking;
    private List<double> _layer0;
    private List<double> _layer1;
    private List<double> _layer1Prev;
    private List<List<double>> _weightHamming;
    protected List<List<double>> _patterns;

    public void Train()
    {
        for (int fi = 0; fi < _patterns.Count; fi++)
        {
            _layer0 = _patterns[fi];
            for (int i = 0; i < _sizeLayer0; i++)
            {
                _weightHamming[fi][i] = _layer0[i] * 0.5;
            }
        }
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
            s += _layer0[i] * _weightHamming[num][i];
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

    public int TestPattern(List<double> pattern)
    {
        _layer0 = pattern;

        StepHamming();
        for (int i = 0; i < MAX_ITER; i++)
        {
            StepHopfield();
            if (StableHopfield())
                break;
        }

        return _layer1.IndexOf(_layer1.Max());
    }
}
