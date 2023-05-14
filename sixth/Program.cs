var x = new BigInteger("1313234242425");
var y = new BigInteger("17898456325");
Console.WriteLine(x + y); // the same as Console.WriteLine(y.Add(x));
if ((y - x).check)
{
    Console.WriteLine("-" + (y - x));
}
else
{
    Console.WriteLine(y - x);

}
/* var z = new BigInteger("97898456325");
//var w = new BigInteger("3488989498");
Console.WriteLine(z.Sub(w)); */

public class BigInteger
{
    private int[] _numbers;
    public bool check;
    
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
        
        var carry = 0;
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
    
    public BigInteger Sub(BigInteger another) 
    {
        // return new BigInteger, result of current - another
        
        int[] firstNum = _numbers;
        int[] secondNum = another._numbers;
        var carry = 0;
        
        if (firstNum.Length < secondNum.Length)
        {
            Array.Resize(ref firstNum, secondNum.Length);
            check = true;
            (firstNum, secondNum) = (secondNum, firstNum);
        }
        else if (secondNum.Length < firstNum.Length)
        {
            Array.Resize(ref secondNum, firstNum.Length);
        }
        else if (firstNum.Length == secondNum.Length)
        {
            for (var i = firstNum.Length-1; i >= 0 ; i--) // check each digit of each number
            {
                if (firstNum[i] < secondNum[i])
                {
                    check = true;
                    (firstNum, secondNum) = (secondNum, firstNum);
                    break;
                }
                if (firstNum[i] > secondNum[i])
                {
                    break;
                }
            }
        }
        
        int[]result = new int[firstNum.Length];
            
        for (int i = 0; i < firstNum.Length; i++)
        {
            result[i] = firstNum[i] - secondNum[i] - carry;
            carry = 0;
            if (result[i] < 0)
            {
                result[i] += 10;
                carry = 1;
            }
        }
        Array.Reverse(result);

        var sub = String.Join("", result);
        var resultBigInteger = new BigInteger(sub);
        if (check)
        {
            resultBigInteger.check = true;
        }
        return resultBigInteger;
    }
    
    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
}
