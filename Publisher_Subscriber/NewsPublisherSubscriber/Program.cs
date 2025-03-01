using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPublisherSubscriber
{

    public class NewsArticle
    {
        public string Title { get; }
        public string Content { get; }
        public NewsArticle(string Title, string Content)
        {
            this.Title = Title;
            this.Content = Content;
        }
    }
    public class NewsPublisher
    {
        public event EventHandler<NewsArticle> NewNewsPublished;
        public void PublishNews(string Title, string Content)
        {
            var Article = new NewsArticle(Title, Content);
            OnNewNewsPublished(Article);
        }
        protected virtual void OnNewNewsPublished(NewsArticle Article)
        {
            NewNewsPublished?.Invoke(this, Article);
        }
    }
    public class NewsSubscriber
    {
        public string Name { get; }
        public NewsSubscriber(string Name)
        {
            this.Name = Name;
        }
        public void Subscribe(NewsPublisher publisher)
        {
            publisher.NewNewsPublished += HandleNewNews;
        }
        public void Unsubscribe(NewsPublisher publisher)
        {
            publisher.NewNewsPublished -= HandleNewNews;
        }
        public void HandleNewNews(object sender, NewsArticle article)
        {
            Console.WriteLine($"{Name} received a new news article:");
            Console.WriteLine($"Title: {article.Title}");
            Console.WriteLine($"Content: {article.Content}");
            Console.WriteLine();  
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            NewsPublisher publisher = new NewsPublisher();
            NewsSubscriber subscriber1 = new NewsSubscriber("Subscriber 1");

            subscriber1.Subscribe(publisher);
            NewsSubscriber subscriber2 = new NewsSubscriber("Subscriber 2");
            subscriber2.Subscribe(publisher);
            publisher.PublishNews("Breaking News", "A significant event just happened!");
            publisher.PublishNews("Tech Update", "New gadgets are hitting the market.");

            //unsubscribe a subscriber (e.g., subscriber1)
            subscriber1.Unsubscribe(publisher);
            publisher.PublishNews("Weather Forecast", "Expect sunny weather for the weekend.");

            subscriber2.Unsubscribe(publisher);
            publisher.PublishNews("Sports Highlights", "A thrilling game just ended!");

            publisher.PublishNews("Final Edition", "Last news update for today.");

            Console.ReadLine();
        }
    }
}
