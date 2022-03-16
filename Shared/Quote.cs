using Newtonsoft.Json;

namespace Shared
{
    public class Quote
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ToJson(Quote q)
        {
            return JsonConvert.SerializeObject(q);
        }

        public Quote ToQuote(string json)
        {
            return JsonConvert.DeserializeObject<Quote>(json);
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Price: " + Price;
        }
    }
}
