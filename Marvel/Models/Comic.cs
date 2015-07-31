using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Marvel.Models
{
    public class Comic
    {
        public int? id;
        public int? digitalId;
        public string title;
        double issueNumber;
        public string variantDescription;
        public string description;
        public DateTime? modified;
        public string isbn;
        public string upc;
        public string diamondCode;
        public string ean;
        public string issn;
        public string format;
        public int? pageCount;
        public TextObject[] textObjects;
        public string resourceURI;
        public Url[] urls;
        public SeriesSummary series;
        public ComicSummary[] variants;
        public ComicSummary[] collections;
        public ComicSummary[] collectedIssues;
        public ComicDate[] dates;
        public ComicPrice[] prices;
        public Image thumbnail;
        public CreatorList creators;
        public CharacterList characters;
        public StoryList stores;
        public EventList events;
    }
}