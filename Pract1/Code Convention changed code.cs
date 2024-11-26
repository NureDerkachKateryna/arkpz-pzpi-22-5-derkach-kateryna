void GeneratePermutations(int[] array)
{
    int counter = 1;
    Output(array, ref counter);

    while (true)
    {
        int i = array.Length - 2;

        while (i != -1 && array[i] > array[i + 1])
        {
            i--;
        }

        if (i == -1)
        {
            break;
        }

        int j = array.Length - 1;

        while (j != -1 && array[i] > array[j])
        {
            j--;
        }

        Swap(ref array[i], ref array[j]);

        ++i;

        for (j = array.Length - 1; i < j; i++, j--)
        {
            Swap(ref array[i], ref array[j]);
        }

        Output(array, ref counter);
    }
}

void Swap(ref int first, ref int second)
{
    int temp = first;
    first = second;
    second = temp;
}

int[] arrayOfNumbers = new int[] { 1, 2, 3, 4 };

void DisplayArray(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.WriteLine(array[i]);
    }
}

namespace Library
{
    public class User
    {
        private const int MAX_ATTEMPTS = 10;
        private int _userId;
        public string? UserName { get; set; }

        public void DisplayUserName(string username)
        {
            Console.WriteLine(username);
        }

    }
}
