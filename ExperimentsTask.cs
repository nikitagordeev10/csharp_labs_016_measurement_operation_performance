using System;
using System.Collections.Generic;

namespace StructBenchmarking {
    public class Experiments {
        public static ChartData BuildChartData(Func<int, ITask> classTask, Func<int, ITask> structTask, IBenchmark benchmark, int repetitionsCount, string chartTitle) {

            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (var i = 16; i <= 512; i *= 2) {
                classesTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(classTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(structTask(i), repetitionsCount)));
            }

            // Результаты измерения вернуть в виде объекта ChartData
            return new ChartData {
                Title = chartTitle,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        // Измеряет длительность работы метода Run у классов StructArrayCreationTask и ClassArrayCreationTask
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount) => BuildChartData(x => new ClassArrayCreationTask(x), x => new StructArrayCreationTask(x), benchmark, repetitionsCount, "Create array");

        // Измеряет длительность работы метода Run у методов MethodCallWithClassArgumentTask и MethodCallWithStructArgumentTask 
        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount) => BuildChartData(x => new MethodCallWithClassArgumentTask(x), x => new MethodCallWithStructArgumentTask(x), benchmark, repetitionsCount, "Create array");
    }
}



/*
 * Материалы:
 * Абстрактная фабрика на пальцах // Хабр URL: https://habr.com/ru/post/465835/ (дата обращения: 18.12.2022).
 * Применение интерфейсов // METANIT.COM URL: https://metanit.com/sharp/tutorial/3.49.php (дата обращения: 18.12.2022).
 * Интерфейсы в C#: зачем они нужны? // Хабр URL: https://habr.com/ru/company/otus/blog/674756/ (дата обращения: 18.12.2022).
 * Use List as parameter in a Func delegate // StackOverflow URL: https://stackoverflow.com/questions/47062740/use-list-as-parameter-in-a-func-delegate (дата обращения: 18.12.2022).
 * Делегаты и лямбда-выражения // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/standard/delegates-lambdas (дата обращения: 18.12.2022).
 * Делегаты и Лямбда выражения в C# .Net — Шпаргалка или коротко о главном // Хабр URL: https://habr.com/ru/post/329886/ (дата обращения: 18.12.2022).
 */