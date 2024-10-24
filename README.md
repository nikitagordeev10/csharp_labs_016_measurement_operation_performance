# Платформа: ulearn
## Курс: Основы программирования
### Тема: 8 Рекурсивные алгоритмы
#### Практика «Benchmark» | «Эксперименты»
##### Решено 17 декабря 2022 в 23:49

В файле BenchmarkTask.cs в классе Benchmark реализован интерфейс IBenchmark

Интерфейс IBenchmark содержит единственный метод, принимающий task — действие, скорость работы которого нужно измерить. И возвращает длительность в миллисекундах.
```csharp
double MeasureDurationInMs(ITask task, ...)
```

В этот метод можно посылать любую реализацию простейшего интерфейса ITask:
public interface ITask
```csharp
{
    void Run();
}
```

Измерить длительность выполнения Run в методе MeasureDurationInMs можно с помощью класса Stopwatch. Однако важно учитывать ряд тонкостей бенчмаркинга:

Современные компьютеры умеют измерять время лишь с некоторой точностью. Поэтому измерять время выполнения одной очень короткой операции бессмысленно. Нужно повторить её множество раз, засечь суммарное время и поделить результат на количество повторений. Для этого метод MeasureDurationInMs принимает вторым аргументом repetitionCount — количество повторений.

Среда исполнения .NET компилирует отдельные методы в машинный код только тогда, когда он понадобился первый раз. Этот подход называется JIT — just in time compilation. Как следствие, первый вызов метода может быть значительно медленнее последующих. Поэтому перед измерением времени нужно сделать один «прогревочный» вызов.

Проект собирается в режиме отладки (Debug) или в режиме релиза (Release). В режиме отладки компилятор не применяет оптимизации кода, что негативно сказывается на производительности. Поэтому перед запуском тестов на производительность переключите сборку проекта в релиз.

В произвольный момент времени выполнение программы может быть приостановлено сборщиком мусора. Это тоже негативно влияет на точность измерений. Минимизировать вероятность этого можно, вызвав сборщик мусора принудительно перед тем, как начинать засекать время. Это можно сделать так:
```csharp
GC.Collect();
GC.WaitForPendingFinalizers();
```

Напишим в том же файле unit-тест в методе StringConstructorFasterThanStringBuilder. В нём нужно сравнить два способа создания строки, состоящей из 10000 букв 'а':

Создаем StringBuilder, много раз вызваем Append, а в конце вызваем у него ToString().
Вызваем специализированный конструктор строки new string('a', 10000).
Постараемся выбрать количество повторений так, чтобы суммарно весь этот тест работал около секунды, чтобы нивелировать описанные выше эффекты.

Тест должен с помощью Assert.Less проверять, что специализированный конструктор строки работает быстрее, чем StringBuilder.

Для каждого метода создания строки создаём свою реализацию ITask в том же файле. Используем её совместно с классом Benchmark в тесте, чтобы сравнить время работы.



В файле ArrayCreationTasks.cs есть две реализации интерфейса ITask для работы с классом Benchmark. Оба класса создают массив в методе Run. Но один делает массив структур, а второй массив классов.

В классе ExperimentsTask реализован метод BuildChartDataForArrayCreation. Этот метод должен измерять длительность работы метода Run у классов StructArrayCreationTask и ClassArrayCreationTask с помощью Benchmark из прошлого задания.

Измерим время для структур и классов всех размеров, указанных в Constants.FieldCounts. Результаты измерения вернем в виде объекта ChartData. Дальше в Program.cs эти результаты будут показаны на графиках.

Запустим код на исполнение. Увидим первый график скорости работы от количества полей в классе/структуре. На нём будет видно, что массивы классов создаются дольше, чем массивы структур.


Аналогично в файле MethodCallTasks.cs есть ещё пара реализаций ITask. Они вызывают метод, передавая в качестве аргумента класс или структуру с большим количеством полей.

В том же классе ExperimentsTask реализуем метод BuildChartDataForMethodCall.

Избавимся от дублирования кода в методах BuildChartDataForMethodCall и BuildChartDataForArrayCreation. Для этого понадобится создать новые классы.

Запустим код на исполнение. Увидим второй график, показывающий, что большие классы передаются в метод быстрее, чем большие структуры.
