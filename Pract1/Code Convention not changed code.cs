int fact(int n)
{
int fact = 1;
if (n != 0)
{
	for (int i = 1; i <= n; i++)
	{
		fact *= i;
	}
}
return fact;
}

void Permutation(int[] arr)
{
    int counter = 1;
    Output(arr, ref counter);

    while (true)
    {
        int i = arr.Length - 2;
        while (i != -1 && arr[i] > arr[i + 1])
        {
            i--;
        }

        if (i == -1)
        {
            break;
        }

        int j = arr.Length - 1;
        while (j != -1 && arr[i] > arr[j])
        {
            j--;
        }
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;

        ++i;
        for (j = arr.Length - 1; i < j; i++, j--)
        {
            int temp2 = arr[i];
            arr[i] = arr[j];
            arr[j] = temp2;
        }
        Output(arr, ref counter);
    }
}

int[] arrayOfNumbers = new int[] { 1, 2, 3, 4 };

void DisplayArray(int[] array)
{
    for (int i = 0; i < 4; i++)
    {
        Console.WriteLine(array[i]);
    }
}

namespace library
{
    public class user
    {
        private const int maxattempts = 10;
        private int Userid;
        public string? username {  get; set; }

        public void displayusername(string Username)
        {
            Console.WriteLine(Username);
        }
    }
}

