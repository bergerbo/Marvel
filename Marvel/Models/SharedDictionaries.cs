using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel.Models
{
    public class TextObject
    {
        public string type;
        public string language;
        public string text;
    }

    public class Url
    {
        public string type;
        public string url;
    }

    public class SeriesSummary
    {
        public string resourceURI;
        public string name;
    }

    public class ComicSummary
    {
        public string resourceURI;
        public string name;
    }

    public class ComicDate
    {
        public string type;
        public DateTime? date;
    }

    public class ComicPrice
    {
        public string type;
        public float? price;
    }

    public class Image
    {
        public string path;
        public string extension;
    }

    public class CreatorList
    {
        public int? available;
        public int? returned;
        public string collectionURI;
        public CreatorSummary[] items;
    }

    public class CreatorSummary
    {
        public string resourceURI;
        public string name;
        public string role;
    }

    public class CharacterList
    {
        public int? available;
        public int? returned;
        public string collectionURI;
        public CharacterSummary[] items;
    }

    public class CharacterSummary
    {
        public string resourceURI;
        public string name;
        public string role;
    }

    public class StoryList
    {
        public int? available;
        public int? returned;
        public string collectionURI;
        public StorySummary[] items;
    }

    public class StorySummary
    {
        public string resourceURI;
        public string name;
        public string type;
    }

    public class EventList
    {
        public int? available;
        public int? returned;
        public string collectionURI;
        public EventSummary[] items;
    }

    public class EventSummary
    {
        public string resourceURI;
        public string name;
    }
}