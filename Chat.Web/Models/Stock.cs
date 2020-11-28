namespace Chat.Web.Models
{
    public class Stock
    {
        public Stock(string name, double close)
        {
            this.Close = close;
            this.Name = name;
        }


        public double Close { get; set; }
        public string Name { get; set; }
    }
}