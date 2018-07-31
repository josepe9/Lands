namespace Lands.Models
{
    using Newtonsoft.Json;
    public class Currency
    {
        //se formatea el json como lo envía el json y como lo llamamos nosotros
        [JsonProperty(PropertyName="code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
    }
}
