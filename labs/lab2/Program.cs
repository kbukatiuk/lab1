using System;
using System.Diagnostics;

class Program
{
    // Структура для збереження статистики сортування
    struct SortStats
    {
        public long Comparisons; // Кількість порівнянь
        public long Swaps;       // Кількість перестановок / записів
        public long TimeMs;      // Час виконання в мілісекундах
    }

    static void Main()
    {
        // Розміри масивів для тестування
        int[] sizes = { 1000, 10000, 50000, 100000 };

        // Генератор випадкових чисел
        Random random = new Random();

        Console.WriteLine("========== BIN SORT ==========\n");

        // Тестуємо алгоритм на масивах різного розміру
        foreach (int size in sizes)
        {
            // Генеруємо випадковий масив у межах від 0 до 99999
            int[] array = GenerateRandomArray(size, 0, 99999, random);

            // Виконуємо сортування та отримуємо статистику
            SortStats stats = BinSortWithStats(array, 99999);

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

    // Метод Bin Sort з підрахунком статистики
    static SortStats BinSortWithStats(int[] array, int maxValue)
    {
        SortStats stats = new SortStats();

        // Таймер для вимірювання часу роботи алгоритму
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Допоміжний масив bins, де індекс = значення елемента,
        // а значення = кількість входжень цього елемента
        int[] bins = new int[maxValue + 1];

        // Розкладаємо елементи масиву по bins
        for (int i = 0; i < array.Length; i++)
        {
            bins[array[i]]++;
            stats.Swaps++; // Рахуємо запис у bins як операцію
        }

        // Індекс для повернення відсортованих елементів назад у масив
        int index = 0;

        // Проходимо по всіх bins у порядку зростання
        for (int value = 0; value <= maxValue; value++)
        {
            // Поки такий елемент зустрічається в bins
            while (bins[value] > 0)
            {
                // Записуємо значення назад у масив
                array[index] = value;
                index++;
                bins[value]--;

                stats.Swaps++; // Рахуємо запис назад у масив як операцію
            }
        }

        // Зупиняємо таймер
        stopwatch.Stop();

        // Зберігаємо час виконання
        stats.TimeMs = stopwatch.ElapsedMilliseconds;

        // Для Bin Sort класичні порівняння між елементами відсутні
        stats.Comparisons = 0;

        return stats;
    }
}