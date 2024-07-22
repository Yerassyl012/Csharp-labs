using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Net.Http;

namespace StockAnalyzer.CS
{

    public static class AsyncEx
    {
        public static async Task<R> Bind<T, R>(this Task<T> task, Func<T, Task<R>> cont)
            => await cont(await task.ConfigureAwait(false)).ConfigureAwait(false);

        public static async Task<R> Map<T, R>(this Task<T> task, Func<T, R> map)
            => map(await task.ConfigureAwait(false));

        public static async Task<R> SelectMany<T, R>(this Task<T> task,
            Func<T, Task<R>> then) => await Bind(task, then);

        public static async Task<R> SelectMany<T1, T2, R>(this Task<T1> task,
            Func<T1, Task<T2>> bind, Func<T1, T2, R> project)
        {
            T1 taskResult = await task;
            return project(taskResult, await bind(taskResult));
        }

        public static async Task<R> Select<T, R>(this Task<T> task, Func<T, R> project)
            => await Map(task, project);

        //Listing 10.2 Refresh of the Otherwise and Retry function
        public static Task<T> Otherwise<T>(this Task<T> task, Func<Task<T>> orTask) =>	// #A
            task.ContinueWith(async innerTask =>
            {
                if (innerTask.Status == TaskStatus.Faulted) return await orTask();
                return await Task.FromResult<T>(innerTask.Result);
            }).Unwrap();

        public static async Task<T> Retry<T>(Func<Task<T>> task, int retries,     // #B
                                TimeSpan delay, CancellationToken cts = default(CancellationToken)) =>
            await task().ContinueWith(async innerTask =>
            {
                cts.ThrowIfCancellationRequested();
                if (innerTask.Status != TaskStatus.Faulted)
                    return innerTask.Result;
                if (retries == 0)
                    throw innerTask.Exception ?? throw new Exception();
                await Task.Delay(delay, cts);
                return await Retry(task, retries - 1, delay, cts);
            }).Unwrap();

        public static async Task<T> Tap<T>(this Task<T> task, Func<Task<T>, Task> operation) // #A
        {
            await operation(task);
            return await task;
        }

        public static async Task<T> Tap<T>(this Task<T> task, Func<T, Task> action)
        {
            await action(await task);
            return await task;
        }
    }
    public struct StockData
    {
        public StockData(DateTime date, double open, double high, double low, double close)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }

        public DateTime Date { get; }
        public Double Open { get; }
        public Double High { get; }
        public Double Low { get; }
        public Double Close { get; }
    }

    public class StockAnalyzer
    {
        public static readonly string[] Stocks =
            new[] { "MSFT", "FB", "AAPL", "YHOO", "EBAY", "INTC", "GOOG", "ORCL" };

        private string CreateFinanceUrl(string symbol) => $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&outputsize=full&apikey=W3LUV5WID6C0PV5L&datatype=csv";

        // Listing 8.4 Stock prices history analysis
            async Task<StockData[]> ConvertStockHistory(string stockHistory)  // #A
            {
                return await Task.Run(() =>
                { // #B
                    string[] stockHistoryRows =
                        stockHistory.Split(Environment.NewLine.ToCharArray(),
                                           StringSplitOptions.RemoveEmptyEntries);
                    return (from row in stockHistoryRows.Skip(1)
                            let cells = row.Split(',')
                            let date = DateTime.Parse(cells[0])
                            let open = double.Parse(cells[1] == "-" ? cells[3] : cells[1])
                            let high = double.Parse(cells[2] == "-" ? cells[4] : cells[2])
                            let low = double.Parse(cells[3])
                            let close = double.Parse(cells[4])
                            select new StockData(date, open, high, low, close)
                           ).ToArray();
                });
            }	// #A

        async Task<string> DownloadStockHistory(string symbol)
        {
            string stockUrl = CreateFinanceUrl(symbol);
            var request = WebRequest.Create(stockUrl);       // #C
            using (var response = await request.GetResponseAsync()
                                              .ConfigureAwait(false)) // #D
            using (var reader = new StreamReader(response.GetResponseStream()))
                return await reader.ReadToEndAsync().ConfigureAwait(false); // #E
        }

        async Task<Tuple<string, StockData[]>> ProcessStockHistory(string symbol)
        {
            string stockHistory = await DownloadStockHistory(symbol);    // #F
            StockData[] stockData = await ConvertStockHistory(stockHistory);  // #F
            return Tuple.Create(symbol, stockData);     // #G
        }

        public async Task AnalyzeStockHistory(string[] stockSymbols)
        {
            var sw = Stopwatch.StartNew();

            IEnumerable<Task<Tuple<string, StockData[]>>> stockHistoryTasks =
              stockSymbols.Select(stock => ProcessStockHistory(stock));   // #H

            var stockHistories = new List<Tuple<string, StockData[]>>();
            foreach (var stockTask in stockHistoryTasks)
                stockHistories.Add(await stockTask);        // #I

            ShowChart(stockHistories, sw.ElapsedMilliseconds);  // #L
        }

        // Listing 8.6 Cancellation of Asynchronous Task
        CancellationTokenSource cts = new CancellationTokenSource();  // #A

        async Task<string> DownloadStockHistory(string symbol,
                                                CancellationToken token)    // #B
        {
            string stockUrl = CreateFinanceUrl(symbol);
            var request = await new HttpClient().GetAsync(stockUrl, token); // #B
            return await request.Content.ReadAsStringAsync();
        }
        //cts.Cancel();  // #C

        async Task AnalyzeStockHistory(string[] stockSymbols,
                                       CancellationToken token)
        {
            var sw = Stopwatch.StartNew();

            //Listing 8.7 Cancellation of Asynchronous operation manual checks
            List<Task<Tuple<string, StockData[]>>> stockHistoryTasks =
                stockSymbols.Select(async symbol =>
                {
                    string stockUrl = CreateFinanceUrl(symbol);
                    var request = HttpWebRequest.Create(stockUrl);
                    using (var response = await request.GetResponseAsync())
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        token.ThrowIfCancellationRequested();

                        var csvData = await reader.ReadToEndAsync();
                        var prices = await ConvertStockHistory(csvData);

                        token.ThrowIfCancellationRequested();
                        return Tuple.Create(symbol, prices.ToArray());
                    }
                }).ToList();

            await Task.WhenAll(stockHistoryTasks)
                .ContinueWith(stockData => ShowChart(stockData.Result, sw.ElapsedMilliseconds), token); // #L
        }

        //Listing 8.10 The Bind operator in action
        async Task<Tuple<string, StockData[]>> ProcessStockHistoryBind(string symbol)
            => await DownloadStockHistory(symbol)
                    .Bind(stockHistory => ConvertStockHistory(stockHistory))  //#A
                    .Bind(stockData => Task.FromResult(Tuple.Create(symbol,
                                                               stockData)));  //#A

        async Task<Tuple<string, StockData[]>> ProcessStockHistoryRetry(string symbol)
        {
            string stockHistory =
                await AsyncEx.Retry(() => DownloadStockHistory(symbol), 5, TimeSpan.FromSeconds(2));
            StockData[] stockData = await ConvertStockHistory(stockHistory);
            return Tuple.Create(symbol, stockData);
        }

        //Listing 8.13 The Or combinator applies to falls back behavior
        Func<string, string> alphavantageSourceUrl = (symbol) => // #A
            $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&outputsize=full&apikey=W3LUV5WID6C0PV5L&datatype=csv";

        Func<string, string> stooqSourceUrl = (symbol) => // #A
            $"https://stooq.com/q/d/l/?s={symbol}.US&i=d";

        async Task<string> DownloadStockHistory(Func<string, string> sourceStock,
                                                                    string symbol)
        {
            string stockUrl = sourceStock(symbol);      // #B
            var request = WebRequest.Create(stockUrl);
            using (var response = await request.GetResponseAsync())
            using (var reader = new StreamReader(response.GetResponseStream()))
                return await reader.ReadToEndAsync();
        }

        async Task<Tuple<string, StockData[]>> ProcessStockHistoryConditional(string symbol)
        {
            Func<Func<string, string>, Func<string, Task<string>>> downloadStock =
                service => stock => DownloadStockHistory(service, stock);  // #C

            Func<string, Task<string>> alphavantageService =
                                    downloadStock(alphavantageSourceUrl); // #D
            Func<string, Task<string>> stoodService =
                                    downloadStock(stooqSourceUrl);  // #D

            return await AsyncEx.Retry( // #E
                        () => alphavantageService(symbol)
                             .Otherwise(() => stoodService(symbol)), //#F
                        5, TimeSpan.FromSeconds(2))
                    .Bind(data => ConvertStockHistory(data))        // #G
                    .Map(prices => Tuple.Create(symbol, prices));   // #H
        }

        //Listing 8.14 Running Stock-History analysis  in parallel
        async Task ProcessStockHistoryParallel()
        {
            var sw = Stopwatch.StartNew();
            List<Task<Tuple<string, StockData[]>>> stockHistoryTasks =
              Stocks.Select(ProcessStockHistory).ToList(); // #A

            Tuple<string, StockData[]>[] stockHistories =
                    await Task.WhenAll(stockHistoryTasks); // #B

            ShowChart(stockHistories, sw.ElapsedMilliseconds);
        }

        //Listing 8.15 Stock-History analysis processing as each Task completes
        public async Task ProcessStockHistoryAsComplete(Chart chart, SynchronizationContext ctx)
        {
            var sw = Stopwatch.StartNew();

            List<Task<Tuple<string, StockData[]>>> stockHistoryTasks =
                Stocks.Select(ProcessStockHistoryConditional).ToList();

            while (stockHistoryTasks.Count > 0) // #A
            {
                Task<Tuple<string, StockData[]>> stockHistoryTask =
                            await Task.WhenAny(stockHistoryTasks);  // #B
                stockHistoryTasks.Remove(stockHistoryTask);  // #C
                Tuple<string, StockData[]> stockHistory = await stockHistoryTask;

                ctx.Send(_ => UpdateChart(chart, stockHistory, sw.ElapsedMilliseconds), null); // #D
                // Thread.Sleep(500);
            }
        }

        private void ShowChart(IEnumerable<Tuple<string, StockData[]>> stockHistories, long elapsedTime)
        {
            // Create a chart containing a default area
            var chart = new Chart { Dock = DockStyle.Fill };
            chart.ChartAreas.Add(new ChartArea("MainArea"));
            chart.Legends.Add(new Legend());
            chart.Titles.Add($"Time elapsed {elapsedTime} ms");

            // Create series and add it to the chart
            foreach (var s in stockHistories)
            {
                var series = new Series
                {
                    LegendText = s.Item1,
                    ChartType = SeriesChartType.Candlestick
                };
                chart.Series.Add(series);

                foreach (var d in s.Item2)
                {
                    series.Points.AddXY(d.Date, d.Open, d.High, d.Low, d.Close);
                }
            }

            // Show chart on the form
            var form = new Form { Visible = true, Width = 700, Height = 500 };
            form.Controls.Add(chart);
            Application.Run(form);
        }

        private Chart CreateChart()
        {
            // Create a chart containing a default area
            var chart = new Chart { Dock = DockStyle.Fill };
            chart.ChartAreas.Add(new ChartArea("MainArea"));
            return chart;
        }

        private void UpdateChart(Chart chart, Tuple<string, StockData[]> stockHistory, long elapsedMilliseconds)
        {
            var series = new Series
            {
                LegendText = stockHistory.Item1,
                ChartType = SeriesChartType.Candlestick
            };
            chart.Series.Add(series);

            foreach (var d in stockHistory.Item2)
                series.Points.AddXY(d.Date, d.Open, d.High, d.Low, d.Close);

            chart.Titles.Clear();
            chart.Titles.Add($"Time elapsed {elapsedMilliseconds} ms");
        }
    }
}