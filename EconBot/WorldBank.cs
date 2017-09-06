using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EconBot
{
    /**
     * Class structure is adapted from - https://stackoverflow.com/questions/36912178/cannot-deserialize-json-data 
     * */
    public class PageModel
    {
        public PageModel()
        {
            this.List = new List<Data>();
        }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("per_page")]
        public string PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        public List<Data> List { get; set; }
    }

    public class Data
    {
        public Data()
        {
            this.Indicator = new Indicator();
            this.Country = new Country();
        }

        [JsonProperty("indicator")]
        public Indicator Indicator { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("decimal")]
        public decimal Decimal { get; set; }

    }

    public class Country
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Indicator
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

}