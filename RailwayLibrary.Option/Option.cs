namespace RailwayLibrary.Option
{
    public abstract class Option<TContent>
    {
        public static Option<TContent> Some(TContent content) => new Some<TContent>(content);

        public static Option<TContent> None() => new None<TContent>();
    }
    
    public class Some<TContent> : Option<TContent>
    {
        public Some(TContent content)
        {
            Content = content;
        }

        public TContent Content { get; }
    }

    public class None<TContent> : Option<TContent> { }
}
