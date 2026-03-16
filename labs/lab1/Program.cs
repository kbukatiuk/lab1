using System;
using System.Diagnostics;

class Program
{
    // Структура для збереження статистики сортування
    struct SortStats
    {
        public long Comparisons; // Кількість порівнянь
        public long Swaps;       // Кількість перестановок
        public long TimeMs;      // Час виконання в мілісекундах
    }

    static void Main()
    {
        // Розміри масивів для тестування
        int[] sizes = { 1000, 10000, 50000, 100000 };

        // Об'єкт для генерації випадкових чисел
        Random random = new Random();

        Console.WriteLine("========== MINMAX SORT ==========\n");

        // Перевіряємо роботу алгоритму на масивах різного розміру
        foreach (int size in sizes)
        {
            // Генеруємо випадковий масив
            int[] array = GenerateRandomArray(size, 0, 100000, random);

            // Виконуємо сортування та отримуємо статистику
            SortStats stats = MinMaxSortWithStats(array);

            // Виводимо результати
            Console.WriteLine($"Розмір масиву: {size}");
            Console.WriteLine($"Порівнянь:     {stats.Comparisons}");
            Console.WriteLine($"Перестановок:  {stats.Swaps}");
            Console.WriteLine($"Час (мс):      {stats.TimeMs}");
            Console.WriteLine(new string('-', 40));
        }
    }

    // Метод для генерації випадкового масиву
    static int[] GenerateRandomArray(int size, int minValue, int maxValue, Random random)
    {
        int[] array = new int[size];

        // Заповнюємо масив випадковими числами
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(minValue, maxValue + 1);
        }

        return array;
    }

    // Метод сортування MinMax з підрахунком статистики
    static SortStats MinMaxSortWithStats(int[] array)
    {
        SortStats stats = new SortStats();

        // Таймер для вимірювання часу виконання
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Ліва та права межі невідсортованої частини масиву
        int left = 0;
        int right = array.Length - 1;

        // Поки межі не перетнулися
        while (left < right)
        {
            // Спочатку вважаємо, що мінімум і максимум знаходяться на позиції left
            int minIndex = left;
            int maxIndex = left;

            // Шукаємо мінімальний і максимальний елемент у поточній частині масиву
            for (int i = left + 1; i <= right; i++)
            {
                // Порівняння для пошуку мінімуму
                stats.Comparisons++;
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }

                // Порівняння для пошуку максимуму
                stats.Comparisons++;
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            // Якщо мінімальний елемент не стоїть на своєму місці, міняємо його місцями
            if (minIndex != left)
            {
                Swap(array, left, minIndex);
                stats.Swaps++;
            }

            // Якщо максимум був на позиції left, то після перестановки мінімуму
            // його індекс треба оновити
            if (maxIndex == left)
            {
                maxIndex = minIndex;
            }

            // Якщо максимальний елемент не стоїть на своєму місці, міняємо його місцями
            if (maxIndex != right)
            {
                Swap(array, right, maxIndex);
                stats.Swaps++;
            }

            // Звужуємо межі пошуку
            left++;
            right--;
        }

        // Зупиняємо таймер
        stopwatch.Stop();

        // Зберігаємо час виконання
        stats.TimeMs = stopwatch.ElapsedMilliseconds;

        return stats;
    }

    // Метод для обміну двох елементів місцями
    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}


