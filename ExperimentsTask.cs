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

            // ���������� ��������� ������� � ���� ������� ChartData
            return new ChartData {
                Title = chartTitle,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        // �������� ������������ ������ ������ Run � ������� StructArrayCreationTask � ClassArrayCreationTask
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount) => BuildChartData(x => new ClassArrayCreationTask(x), x => new StructArrayCreationTask(x), benchmark, repetitionsCount, "Create array");

        // �������� ������������ ������ ������ Run � ������� MethodCallWithClassArgumentTask � MethodCallWithStructArgumentTask 
        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount) => BuildChartData(x => new MethodCallWithClassArgumentTask(x), x => new MethodCallWithStructArgumentTask(x), benchmark, repetitionsCount, "Create array");
    }
}



/*
 * ���������:
 * ����������� ������� �� ������� // ���� URL: https://habr.com/ru/post/465835/ (���� ���������: 18.12.2022).
 * ���������� ����������� // METANIT.COM URL: https://metanit.com/sharp/tutorial/3.49.php (���� ���������: 18.12.2022).
 * ���������� � C#: ����� ��� �����? // ���� URL: https://habr.com/ru/company/otus/blog/674756/ (���� ���������: 18.12.2022).
 * Use List as parameter in a Func delegate // StackOverflow URL: https://stackoverflow.com/questions/47062740/use-list-as-parameter-in-a-func-delegate (���� ���������: 18.12.2022).
 * �������� � ������-��������� // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/standard/delegates-lambdas (���� ���������: 18.12.2022).
 * �������� � ������ ��������� � C# .Net � ��������� ��� ������� � ������� // ���� URL: https://habr.com/ru/post/329886/ (���� ���������: 18.12.2022).
 */