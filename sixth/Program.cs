using System.Net.Mail;

static void QuickSort(int[] arr, int low, int high)
{
    // low - starting index,  high - ending index
    if (low < high)
    {
        int pivotIndex = Partition(arr, low, high); // pivot index is the one we sort around
        QuickSort(arr, low, pivotIndex - 1); // sorting before the index
        QuickSort(arr, pivotIndex + 1, high); // sorting after the index
    }
}

static int Partition(int[] arr, int low, int high) 
{
    // takes last element as pivot and places all the elements smaller than the pivot to its left
    // and greater to its right
    int pivot = arr[high];
    int i = low - 1;

    for (int j = low; j < high; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            Swap(arr, i, j);
        }
    }

    Swap(arr, i + 1, high);
    return i + 1;
    
}

static void PrintArray(int[] arr)
{
    foreach (int element in arr)
    {
        Console.Write(element + " ");
    }
    Console.WriteLine();
}

static void Swap(int[] arr, int i, int j)
{
    (arr[i], arr[j]) = (arr[j], arr[i]);
}

int [] arr = {20, 100, 15, 10, 80, 30, 90, 40, 50, 70};
PrintArray(arr);
QuickSort(arr, 0, arr.Length - 1);
PrintArray(arr);

while (true)
{
    Console.WriteLine("Enter your example (with space):");
    var example = Console.ReadLine().Split(" ");
    var firstN = new BigInteger(example[0]);
    var oper = example[1];
    var secondN = new BigInteger(example[2]);

    Console.WriteLine("The answer is:");
    switch (oper)
    {
        case "+":
            Console.WriteLine($"{firstN + secondN}\n");
            break;
        case "-":
            Console.WriteLine($"{firstN - secondN}\n");
            break;
        case "*":
            Console.WriteLine($"{firstN * secondN}\n");
            break;
    }
}

public class BigInteger
{
    private int[] _numbers;
    private bool IsNegative { get; set; }

    public BigInteger(string value)
    {
        // convert here string representation to inner int array IN REVERSED ORDER
        _numbers = new int[value.Length];
        for (var i = value.Length - 1; i >= 0; i--)
        {
            if (value[i] == '-')
            {
                IsNegative = true;
                Array.Resize(ref _numbers, value.Length -1);
                continue;
            }
            _numbers[value.Length - 1 - i] = int.Parse(value[i].ToString());
        }
    }

    public BigInteger(int[] numbers, bool negative)
    {
        _numbers = numbers;
        IsNegative = negative;
    }
    
    public override string ToString()
    {
        // convert array back to string and return it
        if (_numbers.Length == 0)
        {
            return "0";
        }
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

    private BigInteger Add(BigInteger another)
    {
        // return new BigInteger, result of current + another
        if (IsNegative && !another.IsNegative)
        {
            // If the current number is negative and the other number is positive,
            // perform subtraction instead of addition
            this.IsNegative = !this.IsNegative;
            return another.Sub(this);
        }
        if (!IsNegative && another.IsNegative)
        {
            // If the current number is positive and the other number is negative,
            // perform subtraction instead of addition
            another.IsNegative = !another.IsNegative;
            return Sub(another);
        }
        if (IsNegative && another.IsNegative)
        {
            // If both numbers are negative, perform addition and the result will be negative
            this.IsNegative = !this.IsNegative;
            another.IsNegative = !another.IsNegative;
            var sumAdd = this.Add(another);
            sumAdd.IsNegative = true;
            return sumAdd;
        }

        var carry = 0;
        var firstNum = _numbers;
        var secondNum = another._numbers;

        if (firstNum.Length < secondNum.Length)
        {
            Array.Resize(ref firstNum, secondNum.Length);
        }
        else if (secondNum.Length < firstNum.Length)
        {
            Array.Resize(ref secondNum, firstNum.Length);
        }

        var result = new int[firstNum.Length + 1];

        for (var i = 0; i < firstNum.Length; i++)
        {
            result[i] = firstNum[i] + secondNum[i] + carry;
            carry = result[i] / 10;
            result[i] %= 10;
        }

        if (carry != 0)
        {
            result[^1] = carry;
        }
        
        if (result[^1] == 0)
        {
            var newLength = result.Length - 1;
            var newArray = new int[newLength];

            Array.Copy(result, newArray, newLength);

            result = newArray;
            Array.Reverse(result);
        }
        else
        {
            Array.Reverse(result);
        }

        var stringResult = String.Join("", result);
        var sum = new BigInteger(stringResult);
        sum.IsNegative = IsNegative;
        return sum;
    }

    private BigInteger Sub(BigInteger another)
    {
        // return new BigInteger, result of current - another
        var firstNum = _numbers;
        var secondNum = another._numbers;
        var carry = 0;

        if (!IsNegative && another.IsNegative)
        {
            // If the current number is positive and the other number is negative,
            // perform addition instead of subtraction
            another.IsNegative = !another.IsNegative;
            var sum = Add(another);
            return sum;
        }
        if (IsNegative && !another.IsNegative)
        {
            // If the current number is negative and the other number is positive,
            // perform addition instead of subtraction
            this.IsNegative = !this.IsNegative;
            var sum = another.Add(this);
            sum.IsNegative = true;
            return sum;
        }
        if (IsNegative && another.IsNegative)
        {
            // If both numbers are negative, perform subtraction as positive numbers
            this.IsNegative = !this.IsNegative;
            another.IsNegative = !another.IsNegative;
            return another.Sub(this);
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
            for (var i = firstNum.Length - 1; i >= 0; i--) // check each digit of each number
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

        var result = new int[firstNum.Length];
        for (var i = 0; i < firstNum.Length; i++)
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
        var startIndex = 0;
        while (startIndex < result.Length - 1 && result[startIndex] == 0)
        {
            startIndex++;
        }

        var sub = string.Join("", result[startIndex..]);
        var resultBigInteger = new BigInteger(sub);
        if (IsNegative)
        {
            resultBigInteger.IsNegative = true;
        }

        return resultBigInteger;
    }

    private BigInteger KaratsubaMultiply(BigInteger another)
    {
        // Handle multiplication by 0
        if (ToString() == "0" || another.ToString() == "0")
        {
            return new BigInteger("0");
        }

        // Calculate the number of digits in the operands
        var n = Math.Max(_numbers.Length, another._numbers.Length);
        
        if (_numbers.Length < another._numbers.Length)
        {
            Array.Resize(ref _numbers, another._numbers.Length);
        }
        else if (another._numbers.Length < _numbers.Length)
        {
            Array.Resize(ref another._numbers, _numbers.Length);
        }
        
        // Base case: if the number of digits is less than a threshold, use regular multiplication
        // i.e  a specific condition where the number of digits in the operands is relatively small.
        if (n < 2)
        {
            var res = _numbers[0] * another._numbers[0];
            return new BigInteger(res.ToString());
        }

        // Split the numbers into high and low parts
        var m = n / 2;
        
        

        // var high1 = new BigInteger(ToString().Substring(0, _numbers.Length - m));
        //var h1 = new ArraySegment<int>( _numbers, 0, m );
        var l1 = this._numbers[0..m];
        var low1 = new BigInteger(l1, false);
        
        var h1 = this._numbers[m..];
        var high1 = new BigInteger(h1, this.IsNegative);
        
        var l2 = another._numbers[0..m];
        var low2 = new BigInteger(l2, false);
        
        var h2 = another._numbers[m..];
        var high2 = new BigInteger(h2, another.IsNegative);
        
        // var low1 = new BigInteger(ToString().Substring(_numbers.Length - m));
        // var high2 = new BigInteger(another.ToString().Substring(0, another._numbers.Length - m));
        //var low2 = new BigInteger(another.ToString().Substring(another._numbers.Length - m));

        // Recursive steps
        var z0 = low1.KaratsubaMultiply(low2); // low1 * low2
        var z1 = (low1.Add(high1)).KaratsubaMultiply(low2.Add(high2)); // (low1 + high1) * (low2 + high2)
        var z2 = high1.KaratsubaMultiply(high2); // (high * high2)

        // Calculate the cross term
        var crossTerm = z1.Sub(z2).Sub(z0); // (z1 - z2 - z0)
        
        // Calculate the result
        var result = z2.MultiplyPowerOf10(2 * m).Add(crossTerm.MultiplyPowerOf10(m)).Add(z0); // (z2 × 10 ^ (m2 × 2)) + ((z1 - z2 - z0) × 10 ^ m2) + z0
        
        return result;
    }

    private BigInteger MultiplyPowerOf10(int power)
    {
        if (ToString() == "0")
        {
            return this;
        }

        var result = ToString();
        for (var i = 0; i < power; i++)
        {
            result += "0";
        }
        return new BigInteger(result);
    }

    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    public static BigInteger operator *(BigInteger a, BigInteger b) => a.KaratsubaMultiply(b);
}

/*
static void QuickSort(int[] arr, int low, int high)
{
    // low - starting index,  high - ending index
    if (low < high)
    {
        int pivotIndex = Partition(arr, low, high); // pivot index is the one we sort around
        QuickSort(arr, low, pivotIndex - 1); // sorting before the index
        QuickSort(arr, pivotIndex + 1, high); // sorting after the index
    }
}

static int Partition(int[] arr, int low, int high) 
{
    // takes last element as pivot and places all the elements smaller than the pivot to its left
    // and greater to its right
    int pivot = arr[high];
    int i = low - 1;

    for (int j = low; j < high; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            Swap(arr, i, j);
        }
    }

    Swap(arr, i + 1, high);
    return i + 1;
    
}

static void PrintArray(int[] arr)
{
    foreach (int element in arr)
    {
        Console.Write(element + " ");
    }
    Console.WriteLine();
}

static void Swap(int[] arr, int i, int j)
{
    (arr[i], arr[j]) = (arr[j], arr[i]);
}

int [] arr = {10, 80, 30, 90, 40, 50, 70};
PrintArray(arr);
QuickSort(arr, 0, arr.Length - 1);
PrintArray(arr);
*/