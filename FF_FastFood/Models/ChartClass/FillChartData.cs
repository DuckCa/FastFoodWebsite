using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FF_Fastfood.Models;
namespace FF_Fastfood.Models.ChartClass
{
    
    public class FillChartData
    {
        FF_FastFoodEntities1 db = new FF_FastFoodEntities1();
        public List<DataForChart> FillChart() {
            /*List<DataForChart> dataChart= new List<DataForChart>();
            List<Order> order= db.Orders.ToList();
            DateTime? checkTimeFirst;
            DateTime? checkTimeSecond ;
            decimal amountTotal = 0;
            String converCheckOne= "";
            String converCheckTwo="";
            for(int i=0;i<order.Count; i++)
            {
                checkTimeFirst = order[i].order_date;
                if (checkTimeFirst.HasValue)
                {
                    converCheckOne = checkTimeFirst.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    converCheckOne = null;
                }

                if (converCheckOne != converCheckTwo)
                {
                    amountTotal += order[i].total_amount;
                }
                else
                {
                    DataForChart dataForChart = new DataForChart();
                    dataForChart.order_date = checkTimeFirst.Value.ToString("dd/MM/yyyy");
                    dataForChart.total_amount = amountTotal;
                    dataChart.Add(dataForChart);
                    amountTotal = 0;
                }
                if(i== order.Count - 1)
                {
                    checkTimeSecond = order[i].order_date;
                }else
                {
                    checkTimeSecond = order[i + 1].order_date;
                }
                if (checkTimeSecond.HasValue)
                {
                    converCheckTwo = checkTimeSecond.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    converCheckTwo = null;
                }

            }
            
            return dataChart;*/
            List<DataForChart> dataChart = new List<DataForChart>();
            List<Order> order = db.Orders.ToList();

            // Nhóm các đơn hàng theo ngày và tính tổng số tiền cho mỗi ngày
            var groupedData = order
                .GroupBy(o => o.order_date.Value.Date)
                .Select(g => new DataForChart
                {
                    order_date = g.Key.ToString("dd/MM/yyyy"),
                    total_amount = g.Sum(o => o.total_amount)
                })
                .ToList();

            // Thêm dữ liệu vào dataChart
            dataChart.AddRange(groupedData);

            return dataChart;

        }
    }
}