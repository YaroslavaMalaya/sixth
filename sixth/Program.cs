var x = new BigInteger("1313234242425");
Console.WriteLine(x);

public class BigInteger
{
    private int[] _numbers;

    public BigInteger(string value)
    {
        // convert here string representation to inner int array IN REVERSED ORDER
        _numbers = new int[value.Length];

        for (var i = value.Length - 1; i >= 0; i--)
        {
            _numbers[value.Length - 1 - i] = int.Parse(value[i].ToString());
        }
    }
    
    public override string ToString()
    {
        // convert array back to string and return it
        var str = new List<string>();
        for (var i = _numbers.Length - 1; i >= 0; i--)
        {
            str.Add(Convert.ToString(_numbers[i]));
        }
        var numb = string.Join("", str);

        return numb;
    }
    
    
}
