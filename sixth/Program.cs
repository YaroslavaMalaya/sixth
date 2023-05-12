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
}
