using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

class Program
{
    public static ArrayList arrayList = new ArrayList();
    public static ArrayList tekarray = new ArrayList();
    public static ArrayList çiftarray = new ArrayList();
    public static ArrayList asalarray = new ArrayList();
    public static ArrayList[] dividedArrayLists;
    static ManualResetEvent event1 = new ManualResetEvent(false);
    static ManualResetEvent event2 = new ManualResetEvent(false);
    static ManualResetEvent event3 = new ManualResetEvent(false);
    static ManualResetEvent event4 = new ManualResetEvent(false);

    static void Main(string[] args)
    {
        // 1000000 öğe ekleyerek doldur
        for (int i = 0; i < 1000000; i++)
        {
            arrayList.Add(i);
        }

        // ArrayList'teki öğeleri yazdır
        dividedArrayLists = DivideArrayList(arrayList, 4);
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        Thread thread1 = new Thread(fonk1);
        thread1.Priority = ThreadPriority.Highest;

        Thread thread2 = new Thread(fonk2);
        thread2.Priority = ThreadPriority.AboveNormal;

        Thread thread3 = new Thread(fonk3);
        thread3.Priority = ThreadPriority.Normal;

        Thread thread4 = new Thread(fonk4);
        thread4.Priority = ThreadPriority.BelowNormal;

        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();

        WaitHandle.WaitAll(new WaitHandle[] { event1, event2, event3, event4 }); // Tüm olayların tamamlanmasını bekleyin



        stopwatch.Stop();

        Console.WriteLine($"Toplam thread çalışma süresi: {stopwatch.ElapsedMilliseconds} milisaniye");
        Console.WriteLine("Tek sayı adeti: " + tekarray.Count);
        Console.WriteLine("Çift sayı adeti: " + çiftarray.Count);
        Console.WriteLine("Asal sayı adeti: " + asalarray.Count);


        Console.ReadLine();
    }

    static ArrayList[] DivideArrayList(ArrayList originalList, int parts)
    {
        if (originalList == null)
            throw new ArgumentNullException(nameof(originalList));

        if (parts <= 0)
            throw new ArgumentException("The number of parts must be greater than 0.", nameof(parts));

        int itemsPerPart = originalList.Count / parts;
        ArrayList[] dividedLists = new ArrayList[parts];

        int index = 0;
        for (int i = 0; i < parts; i++)
        {
            int size = (i < originalList.Count % parts) ? itemsPerPart + 1 : itemsPerPart;
            dividedLists[i] = new ArrayList(originalList.GetRange(index, size));
            index += size;
        }

        return dividedLists;
    }

    static void fonk1()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int uzunluk = dividedArrayLists[0].Count;
        for (int i = 0; i < uzunluk; i++)
        {

            int count = 0;
            for (int j = 2; j <= (int)dividedArrayLists[0][i] / 2; j++)
            {
                if ((int)dividedArrayLists[0][i] % j == 0)
                {
                    count++;
                    break; // asal olmadığı anlaşılır anında çık
                }
            }

            if (count == 0)
            {
                if ((int)dividedArrayLists[0][i] != 0 && (int)dividedArrayLists[0][i] != 1)
                {
                    lock (asalarray)
                    {
                        asalarray.Add((int)dividedArrayLists[0][i]);
                    }
                }
            }

            if ((int)dividedArrayLists[0][i] % 2 == 0)
            {
                lock (çiftarray)
                {
                    çiftarray.Add(dividedArrayLists[0][i]);
                }
            }
            else
            {
                lock (tekarray)
                {
                    tekarray.Add(dividedArrayLists[0][i]);
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Thread 1 çalışma süresi: {stopwatch.ElapsedMilliseconds} milisaniye");
        event1.Set();
    }

    static void fonk2()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int uzunluk = dividedArrayLists[1].Count;

        for (int i = 0; i < uzunluk; i++)
        {

            int count = 0;
            for (int j = 2; j <= (int)dividedArrayLists[1][i] / 2; j++)
            {
                if ((int)dividedArrayLists[1][i] % j == 0)
                {
                    count++;
                    break; // asal olmadığı anlaşılır anında çık
                }
            }

            if (count == 0)
            {
                if ((int)dividedArrayLists[1][i] != 0 && (int)dividedArrayLists[1][i] != 1)
                {
                    lock (asalarray)
                    {
                        asalarray.Add((int)dividedArrayLists[1][i]);
                    }
                }
            }

            if ((int)dividedArrayLists[1][i] % 2 == 0)
            {
                lock (çiftarray)
                {
                    çiftarray.Add(dividedArrayLists[1][i]);
                }
            }
            else
            {
                lock (tekarray)
                {
                    tekarray.Add(dividedArrayLists[1][i]);
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Thread 2 çalışma süresi: {stopwatch.ElapsedMilliseconds} milisaniye");

        event2.Set();
    }

    static void fonk3()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int uzunluk = dividedArrayLists[2].Count;

        for (int i = 0; i < uzunluk; i++)
        {

            int count = 0;
            for (int j = 2; j <= (int)dividedArrayLists[2][i] / 2; j++)
            {
                if ((int)dividedArrayLists[2][i] % j == 0)
                {
                    count++;
                    break; // asal olmadığı anlaşılır anında çık
                }
            }

            if (count == 0)
            {
                if ((int)dividedArrayLists[2][i] != 0 && (int)dividedArrayLists[2][i] != 1)
                {
                    lock (asalarray)
                    {
                        asalarray.Add((int)dividedArrayLists[2][i]);
                    }
                }
            }

            if ((int)dividedArrayLists[2][i] % 2 == 0)
            {
                lock (çiftarray)
                {
                    çiftarray.Add(dividedArrayLists[2][i]);
                }
            }
            else
            {
                lock (tekarray)
                {
                    tekarray.Add(dividedArrayLists[2][i]);
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Thread 3 çalışma süresi: {stopwatch.ElapsedMilliseconds} milisaniye");

        event3.Set();
    }

    static void fonk4()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int uzunluk = dividedArrayLists[3].Count;

        for (int i = 0; i < uzunluk; i++)
        {

            int count = 0;
            for (int j = 2; j <= (int)dividedArrayLists[3][i] / 2; j++)
            {
                if ((int)dividedArrayLists[3][i] % j == 0)
                {
                    count++;
                    break; // asal olmadığı anlaşılır anında çık
                }
            }

            if (count == 0)
            {
                if ((int)dividedArrayLists[3][i] != 0 && (int)dividedArrayLists[3][i] != 1)
                {
                    lock (asalarray)
                    {
                        asalarray.Add((int)dividedArrayLists[2][i]);
                    }
                }
            }

            if ((int)dividedArrayLists[3][i] % 2 == 0)
            {
                lock (çiftarray)
                {
                    çiftarray.Add(dividedArrayLists[3][i]);
                }
            }
            else
            {
                lock (tekarray)
                {
                    tekarray.Add(dividedArrayLists[3][i]);
                }
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Thread 4 çalışma süresi: {stopwatch.ElapsedMilliseconds} milisaniye");

        event4.Set();
    }
}
