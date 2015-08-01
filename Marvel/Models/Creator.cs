using System;

namespace Marvel.Models
{
    public class Creator
    {
        public int? id;
        public string firstName;
        public string middleName;
        public string lastName;
        public string suffix;
        public string fullName;
        public string modified;
        public string resourceURI;
        public Url[] urls;
        public Image thumbnail;
        public SeriesList series;
        public StoryList stories;
        public ComicList comics;
        public Comic[] comicsComplete;
        public EventList events;
    }
}