public class Fraction
{
    // Private attributes for the numerator and denominator
    private int _numerator;
    private int _denominator;

    // Constructors
    public Fraction()
    {
        // Default constructor initializing to 1/1
        _numerator = 1;
        _denominator = 1;
    }

    public Fraction(int numerator)
    {
        // Constructor with one parameter (numerator)
        _numerator = numerator;
        _denominator = 1; // Denominator initialized to 1
    }

    public Fraction(int numerator, int denominator)
    {
        // Constructor with both numerator and denominator parameters
        _numerator = numerator;
        _denominator = denominator;
    }

    // Getters and Setters for numerator and denominator
    public int GetNumerator()
    {
        return _numerator;
    }

    public void SetNumerator(int numerator)
    {
        _numerator = numerator;
    }

    public int GetDenominator()
    {
        return _denominator;
    }

    public void SetDenominator(int denominator)
    {
        _denominator = denominator;
    }

    // Method to return fraction as a string (e.g., "3/4")
    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    // Method to return decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)_numerator / _denominator;
    }
}
