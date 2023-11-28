using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;

namespace Sorts
{
    internal class Program
    {
        static void FilePrint(string outputFilePath, Stopwatch sw, int count, int type, int sort, int permutationCounter, int comparisonsCounter)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath, true)) // Установите параметр true для добавления в конец файла
            {
                string sType = "";
                string sSort = "";
                switch(sort)
                {
                    case 1:
                        sSort = "Сортировка простыми вставками";
                        break;
                    case 2:
                        sSort = "Сортировка простым обменом";
                        break;
                    case 3:
                        sSort = "Сортировка простым выбором";
                        break;
                    case 4:
                        sSort = "Шелла (с убывающим шагом)";
                        break;
                    case 5:
                        sSort = "Быстрая сортировка";
                        break;
                }
                switch (type)
                {
                    case 1:
                        sType = "Числа";
                        break;
                    case 2:
                        sType = "Строки";
                        break;
                }
                



                writer.WriteLine("Тип сортировки: " + sSort + " | Тип данных: " + sType + " | Колво данных во входном файле: " + count + " | Затраченное время: " + sw.Elapsed.TotalSeconds + " | Колво перестановок: " + permutationCounter + " | Колво сравнений: " + comparisonsCounter);
            }
        }
        static void PrintArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        static void PrintFlights(string filePath, int[] arr)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("Windows-1251"));
            int[] numbers = arr;
            string[] newLines = new string[lines.Length];

            // Создаем словарь, в котором ключом является номер рейса, а значением - строка из массива lines
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < lines.Length; i++)
            {
                // Ищем подстроку "Номер рейса:"
                int index = lines[i].IndexOf("Номер рейса:");
                if (index != -1)
                {
                    // Вырезаем подстроку после "Номер рейса:" и берем первые 3 символа
                    string substring = lines[i].Substring(index + "Номер рейса: ".Length, 3);

                    // Пытаемся преобразовать полученную подстроку в число
                    if (int.TryParse(substring.Trim(), out int number))
                    {
                        dictionary[number] = lines[i];
                    }
                    else
                    {
                        // Если номер рейса не может быть преобразован в число, то просто пропускаем эту строку
                    }
                }
                else
                {
                    // Если в строке нет подстроки "Номер рейса", то просто пропускаем эту строку
                }
            }

            // Заполняем массив newLines строками из словаря в отсортированном порядке по номерам рейсов
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] >= 0)
                {
                    newLines[i] = dictionary[numbers[i]];
                }
            }

            // Выводим отсортированные строки
            foreach (var item in newLines)
            {
                Console.WriteLine(item);
            }
        }

        static int[] ReadFlightsFromFile(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("Windows-1251"));
            int[] numbers = new int[lines.Length];


            for (int i = 0; i < lines.Length; i++)
            {
                // Ищем подстроку "Номер рейса:"
                int index = lines[i].IndexOf("Номер рейса:");
                if (index != -1)
                {
                    // Вырезаем подстроку после "Номер рейса:" и берем первые 3 символа
                    string substring = lines[i].Substring(index + "Номер рейса: ".Length, 3);

                    // Пытаемся преобразовать полученную подстроку в число
                    if (int.TryParse(substring.Trim(), out int number))
                    {
                        numbers[i] = number;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка в строке: {lines[i]}");
                    }
                }
                else
                {
                    Console.WriteLine($"В строке нет подстроки 'Номер рейса': {lines[i]}");
                }
            }

            return numbers;
        }
        /*static int[] MakeMass(int size)
        {
            Random random = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = random.Next(10000);
            }
            return arr;
        }*/
        static int[] ReadNumbersFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllText(filePath).Split(' '); // Читаем весь файл и разбиваем по пробелам
                int[] numbers = new int[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    if (int.TryParse(lines[i], out int number))
                    {
                        numbers[i] = number;
                    }
                    else
                    {
                        numbers[i] = -1;
                    }
                }

                return numbers;
            }
            catch (Exception)
            {
                //Console.WriteLine($"Произошла ошибка при чтении файла");
                return new int[0];
            }
        }
        static int[] InsertionSort(int[] arr, out int permutationCounter, out int comparisonsCounter) // Сортировка простыми вставками
        {
            int n = arr.Length;
            int[] sorted = new int[n];
            Array.Copy(arr, sorted, n); // Копирование исходного массива
            int permutationCount = 0;
            int comparisonsCount = 0;

            for (int i = 1; i < n; i++)
            {
                int key = sorted[i];
                int j = i - 1;

                // Перемещаем все элементы, большие чем key, на одну позицию вперед
                while (j >= 0 && sorted[j] > key)
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                    permutationCount++;
                    comparisonsCount++;
                }

                // Вставляем key в правильное место
                sorted[j + 1] = key;
                permutationCount++;
            }

            permutationCounter = permutationCount;
            comparisonsCounter = comparisonsCount;
            return sorted;
        }
        static int[] BubbleSort(int[] arr, out int permutationCounter, out int comparisonsCounter) // Сортировка простым обменом
        {
            int n = arr.Length;
            int[] sorted = new int[n];
            Array.Copy(arr, sorted, n); // Копирование исходного массива
            int PermutationCounter = 0;
            int ComparisonsCounter = 0;

            bool swapped; // Переменная, чтобы определить, был ли сделан обмен на текущей итерации

            do
            {
                swapped = false; // Перед каждым проходом сбрасываем флаг обмена

                for (int i = 0; i < n - 1; i++)
                {
                    ComparisonsCounter++; // Увеличиваем счетчик сравнений при каждом сравнении

                    if (sorted[i] > sorted[i + 1])
                    {
                        // Обмен значениями
                        int temp = sorted[i];
                        sorted[i] = sorted[i + 1];
                        sorted[i + 1] = temp;

                        swapped = true; // Устанавливаем флаг, если был сделан обмен
                        PermutationCounter++;
                    }
                }
            } while (swapped); // Продолжаем проходы, пока есть обмены на предыдущей итерации

            permutationCounter = PermutationCounter;
            comparisonsCounter = ComparisonsCounter;
            return sorted;
        }
        static int[] SelectionSort(int[] arr, out int permutationCounter, out int comparisonsCounter) // Сортировка простым выбором
        {
            int n = arr.Length;
            int[] sorted = new int[n];
            Array.Copy(arr, sorted, n); // Копирование исходного массива
            int PermutationCounter = 0;
            int ComparisonsCounter = 0;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                // Находим индекс минимального элемента в неотсортированной части массива
                for (int j = i + 1; j < n; j++)
                {
                    ComparisonsCounter++; // Увеличиваем счетчик сравнений при каждом сравнении элементов

                    if (sorted[j] < sorted[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Обмен значениями
                if (minIndex != i)
                {
                    int temp = sorted[i];
                    sorted[i] = sorted[minIndex];
                    sorted[minIndex] = temp;

                    PermutationCounter++;
                }
            }

            permutationCounter = PermutationCounter;
            comparisonsCounter = ComparisonsCounter;
            return sorted;
        }
        static int[] ShellSort(int[] arr, out int permutationCounter, out int comparisonsCounter) // Шелла (с убывающим шагом)
        {
            int n = arr.Length;
            int[] sorted = new int[n];
            Array.Copy(arr, sorted, n); // Копирование исходного массива
            int PermutationCounter = 0;
            int ComparisonsCounter = 0;

            int gap = n / 2; // Начальный шаг

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = sorted[i];
                    int j = i;

                    // Сравниваем элементы на расстоянии gap и обмениваем их, если необходимо
                    while (j >= gap && sorted[j - gap] > temp)
                    {
                        sorted[j] = sorted[j - gap];
                        j -= gap;
                        PermutationCounter++;
                        ComparisonsCounter++;
                    }

                    sorted[j] = temp;
                }

                gap /= 2; // Уменьшаем шаг
            }

            permutationCounter = PermutationCounter;
            comparisonsCounter = ComparisonsCounter;
            return sorted;
        }



        static int comparisons = 0;
        static int swaps = 0;
        static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(arr, low, high);

                QuickSort(arr, low, partitionIndex - 1);
                QuickSort(arr, partitionIndex + 1, high);
            }
        }
        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                comparisons++;
                if (arr[j] < pivot)
                {
                    i++;

                    int temp = arr[i];
                    arr[i] = arr[j]; // элементы меньшие опорного перемещаются влево
                    arr[j] = temp;

                    swaps++;
                }
            }

            int temp2 = arr[i + 1];
            arr[i + 1] = arr[high]; // Обменивает опорный элемент с элементом, расположенным после всех элементов, меньших опорного
            arr[high] = temp2;

            swaps++;

            return i + 1; // индекс опорного элемента
        }



        static void Main(string[] args)
        {
            string outputFilePath = "output.txt";
            string filePath = "";
            while (true)
            {
                int choice1, choice2, choice3, n = 500;
                int[] array;
                Stopwatch sw = new Stopwatch();
                Console.Clear();
                Console.WriteLine("1 - Сортировка простыми вставками");
                Console.WriteLine("2 - Сортировка простым обменом");
                Console.WriteLine("3 - Сортировка простым выбором");
                Console.WriteLine("4 - Шелла (с убывающим шагом)");
                Console.WriteLine("5 - Быстрая сортировка");
                Console.WriteLine("6 - Выход");
                int.TryParse(Console.ReadLine(), out choice1);
                switch (choice1)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("1 - Числа");
                        Console.WriteLine("2 - Строки");
                        int.TryParse(Console.ReadLine(), out choice2);
                        switch (choice2)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - 500");
                                Console.WriteLine("2 - 1000");
                                Console.WriteLine("3 - 5000");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 500;
                                        filePath = "500.txt";
                                        break;
                                    case 2:
                                        n = 1000;
                                        filePath = "1000.txt";
                                        break;
                                    case 3:
                                        n = 5000;
                                        filePath = "5000.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadNumbersFromFile(filePath);
                                PrintArray(array);
                                sw.Start();
                                int permutationCounter, comporationsCounter;
                                int[] insertionSort = InsertionSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintArray(insertionSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("1 - 25");
                                Console.WriteLine("2 - 50");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 25;
                                        filePath = "25.txt";
                                        break;
                                    case 2:
                                        n = 50;
                                        filePath = "50.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadFlightsFromFile(filePath);
                                PrintFlights(filePath, array);
                                sw.Start();
                                insertionSort = InsertionSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintFlights(filePath, insertionSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            default:
                                Console.Clear();
                                Main(args);
                                break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1 - Числа");
                        Console.WriteLine("2 - Строки");
                        int.TryParse(Console.ReadLine(), out choice2);
                        switch (choice2)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - 500");
                                Console.WriteLine("2 - 1000");
                                Console.WriteLine("3 - 5000");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 500;
                                        filePath = "500.txt";
                                        break;
                                    case 2:
                                        n = 1000;
                                        filePath = "1000.txt";
                                        break;
                                    case 3:
                                        n = 5000;
                                        filePath = "5000.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadNumbersFromFile(filePath);
                                PrintArray(array);
                                sw.Start();
                                int permutationCounter, comporationsCounter;
                                int[] bubbleSort = BubbleSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintArray(bubbleSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("1 - 25");
                                Console.WriteLine("2 - 50");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 25;
                                        filePath = "25.txt";
                                        break;
                                    case 2:
                                        n = 50;
                                        filePath = "50.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadFlightsFromFile(filePath);
                                PrintFlights(filePath, array);
                                sw.Start();
                                bubbleSort = BubbleSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintFlights(filePath, bubbleSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            default:
                                Console.Clear();
                                Main(args);
                                break;
                        }
                        break; 
                    case 3:
                        Console.Clear();
                        Console.WriteLine("1 - Числа");
                        Console.WriteLine("2 - Строки");
                        int.TryParse(Console.ReadLine(), out choice2);
                        switch (choice2)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - 500");
                                Console.WriteLine("2 - 1000");
                                Console.WriteLine("3 - 5000");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 500;
                                        filePath = "500.txt";
                                        break;
                                    case 2:
                                        n = 1000;
                                        filePath = "1000.txt";
                                        break;
                                    case 3:
                                        n = 5000;
                                        filePath = "5000.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadNumbersFromFile(filePath);
                                PrintArray(array);
                                sw.Start();
                                int permutationCounter, comporationsCounter;
                                int[] selectionSort = SelectionSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintArray(selectionSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("1 - 25");
                                Console.WriteLine("2 - 50");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 25;
                                        filePath = "25.txt";
                                        break;
                                    case 2:
                                        n = 50;
                                        filePath = "50.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadFlightsFromFile(filePath);
                                PrintFlights(filePath, array);
                                sw.Start();
                                selectionSort = SelectionSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintFlights(filePath, selectionSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            default:
                                Main(args);
                                break;
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("1 - Числа");
                        Console.WriteLine("2 - Строки");
                        int.TryParse(Console.ReadLine(), out choice2);
                        switch (choice2)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - 500");
                                Console.WriteLine("2 - 1000");
                                Console.WriteLine("3 - 5000");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 500;
                                        filePath = "500.txt";
                                        break;
                                    case 2:
                                        n = 1000;
                                        filePath = "1000.txt";
                                        break;
                                    case 3:
                                        n = 5000;
                                        filePath = "5000.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadNumbersFromFile(filePath);
                                PrintArray(array);
                                sw.Start();
                                int permutationCounter, comporationsCounter;
                                int[] shellSort = ShellSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintArray(shellSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("1 - 25");
                                Console.WriteLine("2 - 50");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 25;
                                        filePath = "25.txt";
                                        break;
                                    case 2:
                                        n = 50;
                                        filePath = "50.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadFlightsFromFile(filePath);
                                PrintFlights(filePath, array);
                                sw.Start();
                                shellSort = ShellSort(array, out permutationCounter, out comporationsCounter);
                                sw.Stop();
                                Console.WriteLine("Массив после изменения:");
                                PrintFlights(filePath, shellSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, permutationCounter, comporationsCounter);
                                Console.ReadKey(true);
                                break;
                            default:
                                Console.Clear();
                                Main(args);
                                break;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("1 - Числа");
                        Console.WriteLine("2 - Строки");
                        int.TryParse(Console.ReadLine(), out choice2);
                        switch (choice2)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - 500");
                                Console.WriteLine("2 - 1000");
                                Console.WriteLine("3 - 5000");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 500;
                                        filePath = "500.txt";
                                        break;
                                    case 2:
                                        n = 1000;
                                        filePath = "1000.txt";
                                        break;
                                    case 3:
                                        n = 5000;
                                        filePath = "5000.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadNumbersFromFile(filePath);
                                PrintArray(array);
                                int[] quickSort = new int[n];
                                Array.Copy(array, quickSort, n);
                                sw.Start();
                                QuickSort(quickSort, 0, quickSort.Length - 1);
                                sw.Stop();
                                Console.WriteLine($"Массив после изменения");
                                PrintArray(quickSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, swaps, comparisons);
                                comparisons = 0;
                                swaps = 0;
                                Console.ReadKey(true);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("1 - 25");
                                Console.WriteLine("2 - 50");
                                int.TryParse(Console.ReadLine(), out choice3);
                                switch (choice3)
                                {
                                    case 1:
                                        n = 25;
                                        filePath = "25.txt";
                                        break;
                                    case 2:
                                        n = 50;
                                        filePath = "50.txt";
                                        break;
                                    default:
                                        Main(args);
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Массив до изменения:");
                                array = ReadFlightsFromFile(filePath);
                                PrintFlights(filePath, array);
                                quickSort = new int[n];
                                Array.Copy(array, quickSort, n);
                                sw.Start();
                                QuickSort(quickSort, 0, quickSort.Length - 1);
                                sw.Stop();
                                Console.WriteLine($"Массив после изменения");
                                PrintFlights(filePath, quickSort);
                                FilePrint(outputFilePath, sw, n, choice2, choice1, swaps, comparisons);
                                comparisons = 0;
                                swaps = 0;
                                Console.ReadKey(true);
                                break;
                            default:
                                Console.Clear();
                                Main(args);
                                break;
                        }
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Main(args);
                        break;
                }
            }
        }
    }
}