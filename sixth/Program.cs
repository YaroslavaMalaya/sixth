var x = new BigInteger("1313234242425");
var y = new BigInteger("17898456325");
Console.WriteLine(x);
Console.WriteLine(y.Add(x));

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
    
    public BigInteger Add(BigInteger another)
    {
        // return new BigInteger, result of current + another
        
        int carry = 0;
        int[] firstNum = _numbers;
        int[] secondNum = another._numbers;

        if (firstNum.Length < secondNum.Length)
        {
            Array.Resize(ref firstNum, secondNum.Length);
        }
        
        else if (secondNum.Length < firstNum.Length)
        {
            Array.Resize(ref secondNum, firstNum.Length);
        }
        
        int[]result = new int[firstNum.Length + 1];
        
        for (int i = 0; i < firstNum.Length; i++)
        {
            result[i] = firstNum[i] + secondNum[i] + carry;
            carry = 0;
            if (result[i] >= 10)
            {
                result[i] -= 10;
                carry = 1;
            }
        }
        result[^1] = carry;
        
        if (result[^1] == 0)
        {
            int newLength = result.Length - 1;
            int[] newArray = new int[newLength];

            Array.Copy(result, newArray, newLength);

            result = newArray;
            Array.Reverse(result);
        }
        
        var stringResult = String.Join("", result);
        return new BigInteger(stringResult);
    }
    
    
}
