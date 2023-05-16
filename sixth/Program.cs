﻿/*
while (true)
{
    Console.WriteLine("Enter your example (with space):");
    var example = Console.ReadLine().Split(" ");
    var firstN = example[0];
    var oper = example[1];
    var secondN = example[2];

    if (firstN.Contains('-') || secondN.Contains('-'))
    {
        
    }

}
*/
// var x = new BigInteger("1313234242425");
// var y = new BigInteger("17898456325");

var x = new BigInteger("25");
var y = new BigInteger("-4631");
Console.WriteLine(x + y); // the same as Console.WriteLine(y.Add(x));
Console.WriteLine(y - x);
Console.WriteLine(x - y);
Console.WriteLine(x * y);

public class BigInteger
{
    private int[] _numbers;
    public bool IsNegative { get; private set; }

    public BigInteger(string value)
    {
        // convert here string representation to inner int array IN REVERSED ORDER
        _numbers = new int[value.Length];

        for (var i = value.Length - 1; i >= 0; i--)
        {
            if (value[i] == '-')
            {
                IsNegative = true;
                continue;
            }
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
        if (IsNegative)
        {
            numb = "-" + numb;
        }
        return numb;
    }
    
    public string ToStringNegative()
    {
        // convert array back to string and return it
        var str = new List<string>();
        for (var i = _numbers.Length - 2; i >= 0; i--)
        {
            str.Add(Convert.ToString(_numbers[i]));
        }

        var numb = string.Join("", str);
        // Console.WriteLine(numb);

        return numb;
    }
    
    public BigInteger Add(BigInteger another)
    {
        // return new BigInteger, result of current + another
        
        if (IsNegative && !another.IsNegative)
        {
            // If the current number is negative and the other number is positive,
            // perform subtraction instead of addition
            BigInteger negativeThis = new BigInteger(ToStringNegative());
            negativeThis.IsNegative = false;
            return another.Sub(negativeThis);
        }

        else if (!IsNegative && another.IsNegative)
        {
            // If the current number is positive and the other number is negative,
            // perform subtraction instead of addition
            BigInteger negativeAnother = new BigInteger(another.ToStringNegative());
            negativeAnother.IsNegative = false;
            return Sub(negativeAnother);
        }

        else if (IsNegative && another.IsNegative)
        {
            // If both numbers are negative, perform addition and the result will be negative
            BigInteger negativeThis = new BigInteger(ToStringNegative());
            negativeThis.IsNegative = false;
            BigInteger negativeAnother = new BigInteger(another.ToStringNegative());
            negativeAnother.IsNegative = false;
            BigInteger sumAdd = negativeThis.Add(negativeAnother);
            sumAdd.IsNegative = true;
            return sumAdd;
        }
        
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
        else
        {
            Array.Reverse(result);
        }
        var stringResult = String.Join("", result);
        BigInteger sum = new BigInteger(stringResult);
        sum.IsNegative = IsNegative;

        return sum;
    }
    
    public BigInteger Sub(BigInteger another) 
    {
        // return new BigInteger, result of current - another
        
        int[] firstNum = _numbers;
        int[] secondNum = another._numbers;
        var carry = 0;
        
        if (!IsNegative && another.IsNegative)
        {
            // If the current number is positive and the other number is negative,
            // perform addition instead of subtraction
            BigInteger negativeAnother = new BigInteger(another.ToStringNegative());
            negativeAnother.IsNegative = false;
            BigInteger sum = Add(another);
            sum.IsNegative = true;
            return sum;
        }
        else if (IsNegative && !another.IsNegative)
        {
            // If the current number is negative and the other number is positive,
            // perform addition instead of subtraction
            BigInteger negativeThis = new BigInteger(ToStringNegative());
            negativeThis.IsNegative = false;
            BigInteger sum = negativeThis.Add(another);
            sum.IsNegative = true;
            return sum;
        }
        else if (IsNegative && another.IsNegative)
        {
            // If both numbers are negative, perform subtraction as positive numbers
            BigInteger negativeThis = new BigInteger(ToStringNegative());
            negativeThis.IsNegative = false;
            BigInteger negativeAnother = new BigInteger(another.ToStringNegative());
            negativeAnother.IsNegative = false;
            return negativeAnother.Sub(negativeThis);
        }
        
        if (firstNum.Length < secondNum.Length)
        {
            Array.Resize(ref firstNum, secondNum.Length);
            IsNegative = true;
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
                    IsNegative = true;
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

        // Remove leading zeros
        int startIndex = 0;
        while (startIndex < result.Length - 1 && result[startIndex] == 0)
        {
            startIndex++;
        }
        
        var sub = String.Join("", result);
        var resultBigInteger = new BigInteger(sub);
        if (IsNegative)
        {
            resultBigInteger.IsNegative = true;
        }
        return resultBigInteger;
    }
    
    
    public BigInteger MultKaratsuba(BigInteger another)
    {
        // return new BigInteger, result of current * another
        int[] firstNum = _numbers; 
        int[] secondNum = another._numbers;
        var power = Math.Max(firstNum.Length, secondNum.Length) - 1; 
        int bm = (int)Math.Pow(10, power);

        // це множення багатозначного числа на однозначне (а це є декілька разів додати)
        if (firstNum.Length == 1)
        {
            var first = int.Parse(String.Join("", firstNum));
            Array.Reverse(secondNum);
            var number = new BigInteger(String.Join("", secondNum));
            BigInteger res = new BigInteger("0");
            for (int i = first; i > 0; i--)
            {
                res += number;
            }
            return res;
        }
        if (secondNum.Length == 1)
        {
            var first = int.Parse(String.Join("", secondNum));
            Array.Reverse(firstNum);
            BigInteger number1 = new BigInteger(String.Join("", firstNum));
            BigInteger res1 = new BigInteger("0");
            for (int i = first; i > 0; i--)
            {
                res1 += number1;
            }
            return res1;
        }
            
        // це треба для того, щоб якщо це число і є найменшим по довжині, то ми могли взяти його 1 елемент спокійно (далі буде більш зрозуміло)
        // var carryX = 0;
        // var carryY = 0;
        // if (firstNum.Length - power == 0)
        // {
        //     carryX = 1;
        // }
        // if (secondNum.Length - power == 0)
        // {
        //     carryY = 1;
        // }
        
        // var x1 = new int[firstNum.Length - power + carryX]; // отут як і казала, завжди буде елемент 
        // var x2 = new int[power - carryX]; // відняти від степеня, бо інакше там будуть усі цифри навіть (з першим х1) ЯКЩО це знову ж таки найменше число по довжині
        // var y1 = new int[secondNum.Length - power + carryY];
        // var y2 = new int[power - carryY];
        //
        // Array.Copy(firstNum, power - carryX, x1, 0, firstNum.Length - power + carryX);
        // Array.Copy(firstNum, 0, x2, 0, power - carryX);
        // Array.Copy(secondNum, power - carryY, y1, 0, secondNum.Length - power + carryY);
        // Array.Copy(secondNum, 0, y2, 0, power - carryY);
        
        
        var x1 = new int[firstNum.Length - power ]; // отут як і казала, завжди буде елемент 
        var x2 = new int[power]; // відняти від степеня, бо інакше там будуть усі цифри навіть (з першим х1) ЯКЩО це знову ж таки найменше число по довжині
        var y1 = new int[secondNum.Length - power];
        var y2 = new int[power];
        
        Array.Copy(firstNum, power, x1, 0, firstNum.Length - power);
        Array.Copy(firstNum, 0, x2, 0, power );
        Array.Copy(secondNum, power, y1, 0, secondNum.Length - power );
        Array.Copy(secondNum, 0, y2, 0, power);

        Array.Reverse(x1);
        var x11 = new BigInteger(String.Join("", x1)); 
        Array.Reverse(x2);
        var x22 = new BigInteger(String.Join("", x2));
        Array.Reverse(y1);
        var y11 = new BigInteger(String.Join("", y1));
        Array.Reverse(y2);
        var y22 = new BigInteger(String.Join("", y2));
        
        var z0 = x22 * y22;
        var z2 = x11 * y11;
        var z1 = (x11 + x22)*(y11 + y22) - z2 - z0;
        
        // це по суті множення будь-якого числа на bm (на пряму не зможем помножити) 
        var z22 = z2.ToString();
        foreach (char el in Math.Pow(bm, 2).ToString())
        {
            if (el != '1')
            {
                z22 += "0";
            }
        }
        var z11 = z1.ToString();
        foreach (char el in bm.ToString())
        {
            if (el != '1')
            {
                z11 += "0";
            }
        }
        
        var result = new BigInteger(z22) + new BigInteger(z11) + z0;

        return result;
    }

    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    public static BigInteger operator *(BigInteger a, BigInteger b) => a.MultKaratsuba(b);
    
    public BigInteger KaratsubaVer2(BigInteger another)
    {
        int[] firstNum = _numbers; 
        int[] secondNum = another._numbers;
        var result = new BigInteger("0");
        

        if (firstNum.Length == 1 || secondNum.Length == 1)
        {
            var res = firstNum[0] * secondNum[0];
            var num = new BigInteger(res.ToString());
            result += num;
            Console.WriteLine(result);
            return result;
        }
        else
        {
            var size = Math.Max(firstNum.Length, secondNum.Length);
            var m = size / 2;
            int bm = (int)Math.Pow(10, m);
        
            /* Split the digit sequences in the middle. */
            var x1 = firstNum.Take(m).ToArray();
            var y1 = firstNum.Skip(m).
                Take(firstNum.Length - m).ToArray();
        
            var x2 = secondNum.Take(m).ToArray();
            var y2 = secondNum.Skip(m).
                Take(secondNum.Length - m).ToArray();
        
            Array.Reverse(x1);
            var x11 = new BigInteger(String.Join("", x1)); 
            Array.Reverse(y1);
            var y11 = new BigInteger(String.Join("", y1)); 
            Array.Reverse(x2);
            var x12 = new BigInteger(String.Join("", x2));
            Array.Reverse(y2);
            var y12 = new BigInteger(String.Join("", y2)); 
        

            var z0 = y11.KaratsubaVer2(y12);
            var z1 = (x11 + y11).KaratsubaVer2(x12 + y12);
            var z2 = x11.KaratsubaVer2(x12);

            var ten = new BigInteger("10");
        
            /*
            int number = z2 * ten ^ (size * 2) + ((z1 - z2 - z0) * ten ^ m) + z0;
            var numb = new BigInteger(number.ToString());
            result += numb;*/
        
            var z22 = z2.ToString();
            foreach (char el in Math.Pow(size, 2).ToString())
            {
                if (el != '1')
                {
                    z22 += "0";
                }
            }
            var z11 = z1.ToString();
            foreach (char el in bm.ToString())
            {
                if (el != '1')
                {
                    z11 += "0";
                }
            }
        
            result = new BigInteger(z22) + new BigInteger(z11) + z0;
        
            return result;
        }
    }
}
