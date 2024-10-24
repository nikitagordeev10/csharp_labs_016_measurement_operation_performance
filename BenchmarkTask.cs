using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace StructBenchmarking {

    /**********************************  Benchmark  **********************************/
    // реализуйте в классе Benchmark интерфейс IBenchmark.
    

    public class Benchmark : IBenchmark {

        // метод, принимающий task — действие, скорость работы которого нужно измерить.
        // repetitionCount — количество повторений
        public double MeasureDurationInMs(ITask task, int repetitionCount) {          
            GC.Collect();                   // уменьшить вероятность, что Garbadge Collector
            GC.WaitForPendingFinalizers();  // как-то повлияет на 

            // перед измерением времени один «прогревочный» вызов
            task.Run();

            // Измерить длительность выполнения Run можно с помощью класса Stopwatch
            var stopwatch = Stopwatch.StartNew();

            // Нужно повторить её множество раз
            for (var i = 0; i < repetitionCount; i++) { 
                task.Run();
            }

            stopwatch.Stop();

            // поделить суммарное время на количество повторений. 
            return stopwatch.ElapsedMilliseconds / (double)repetitionCount;
        }
    }

    /**********************************  методы создания строки  **********************************/
    // Для каждого метода создания строки создайте свою реализацию ITask

    // Способ 1 (StringBuilder + Append + ToString)
    public class CreateStringBuilderString : ITask {
        public void Run() {
            // Создать StringBuilder
            var stringBuilderString = new StringBuilder();

            // много раз вызвать Append
            for (var i = 0; i < 10000; i++)
                stringBuilderString.Append('a');

            // в конце вызвать у него ToString()
            stringBuilderString.ToString();
        }
    }

    // Способ 2 (специализированный конструктор строки)
    public class CreateStringConstructorString : ITask {
        public void Run() {
            // специализированный конструктор строки
            new string('a', 10000);
        }
    }

    /**********************************  unit-тест  **********************************/

    [TestFixture]
    public class RealBenchmarkUsageSample {
        [Test]
        public void StringConstructorFasterThanStringBuilder() {
            // количество повторений
            var repetitionCount = 10000;

            // способы создания строки
            var stringBuilderString = new CreateStringBuilderString();
            var stringConstructorString = new CreateStringConstructorString();

            // Benchmark, чтобы сравнить время работы
            var benchmark = new Benchmark();

            // время выполнения способов
            var methodOneTime = benchmark.MeasureDurationInMs(stringBuilderString, repetitionCount);
            var methodTwoTime = benchmark.MeasureDurationInMs(stringConstructorString, repetitionCount);

            // проверка, что специализированный конструктор строки работает быстрее, чем StringBuilder.
            Assert.Less(methodTwoTime, methodOneTime);
        }
    }
}

/* 
 * Материалы
 * NotImplementedException Класс // Microsoft URL: https://learn.microsoft.com/ru-RU/dotnet/api/system.notimplementedexception?view=netstandard-1.3 (дата обращения: 17.12.2022).
 * Для чего нужен throw new NotImplementedException() // StackOverflow URL: https://ru.stackoverflow.com/questions/451540/Для-чего-нужен-throw-new-notimplementedexception (дата обращения: 17.12.2022).
 * Stopwatch.ElapsedMilliseconds Свойство // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/api/system.diagnostics.stopwatch.elapsedmilliseconds?view=net-7.0 (дата обращения: 17.12.2022).
 * Замеряем производительность с помощью BenchmarkDotNet // Хабр URL: https://habr.com/ru/post/277177/ (дата обращения: 17.12.2022).
 * Difference between ElapsedTicks, ElapsedMilliseconds, Elapsed.Milliseconds and Elapsed.TotalMilliseconds? (C#) // StackOverflow URL: https://stackoverflow.com/questions/8894425/difference-between-elapsedticks-elapsedmilliseconds-elapsed-milliseconds-and-e (дата обращения: 17.12.2022).
 * Regular expression for decimal number // StackOverflow URL: https://stackoverflow.com/questions/968825/regular-expression-for-decimal-number (дата обращения: 17.12.2022).
 * Приостановка и прерывание потоков // microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/standard/threading/pausing-and-resuming-threads (дата обращения: 17.12.2022).
 * When to use Task.Delay, when to use Thread.Sleep? // StackOverflow URL: https://stackoverflow.com/questions/20082221/when-to-use-task-delay-when-to-use-thread-sleep (дата обращения: 17.12.2022).
 */