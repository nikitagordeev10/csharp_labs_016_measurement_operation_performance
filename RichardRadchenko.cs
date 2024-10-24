using System.Collections.Generic;

namespace StructBenchmarking {
    public class Experiments {

        public interface BuildChartData { }

        public static ChartData BuildChartData(
            ITask firstTask, ITask secondTask, IBenchmark benchmark, string title, int repetitionsCount) {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var i in Constants.FieldCounts) {
                var classTest = (ITask)Activator.CreateInstance(firstTask.GetType(), i);
                var structTest = (ITask)Activator.CreateInstance(secondTask.GetType(), i);

                var firstTest = benchmark.MeasureDurationInMs(classTest, repetitionsCount);
                var secondTest = benchmark.MeasureDurationInMs(structTest, repetitionsCount);

                classesTimes.Add(new ExperimentResult(i, firstTest));
                structuresTimes.Add(new ExperimentResult(i, secondTest));
            }

            return new ChartData {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }

    public static void Main() {
        var arraysData = Experiments.BuildChartData(new ClassArrayCreationTask(0),
                                                    new StructArrayCreationTask(0),
                                                    new Benchmark(),
                                                    "Array creation",
                                                    100);
        var callsData = Experiments.BuildChartData(new MethodCallWithStructArgumentTask(0),
                                                    new MethodCallWithClassArgumentTask(0),
                                                    new Benchmark(),
                                                    "Method calls",
                                                    1000000);
        var form = CreateChartForm(arraysData, callsData);
        Application.Run(form);
    }
}