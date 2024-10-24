using System.Collections.Generic;

namespace StructBenchmarking {
    public class Experiments {

        public interface BuildChartData { }

        // В классе ExperimentsTask реализуйте метод BuildChartDataForArrayCreation
        // должен измерять длительность работы метода Run у классов StructArrayCreationTask и ClassArrayCreationTask
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount) {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (var i = 16; i <= 512; i *= 2) {
                classesTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(new ClassArrayCreationTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(new StructArrayCreationTask(i), repetitionsCount)));
            }

            // Результаты измерения вернуть в виде объекта ChartData
            return new ChartData {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
        // метод BuildChartDataForMethodCall
        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount) {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (var i = 16; i <= 512; i *= 2) {
                classesTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(i), repetitionsCount)));
            }

            return new ChartData {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}

/*
 * Материалы:
 * Абстрактная фабрика на пальцах // Хабр URL: https://habr.com/ru/post/465835/ (дата обращения: 18.12.2022).
 * Применение интерфейсов // METANIT.COM URL: https://metanit.com/sharp/tutorial/3.49.php (дата обращения: 18.12.2022).
 */